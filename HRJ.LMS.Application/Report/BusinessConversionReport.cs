using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Application.User;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class BusinessConversionReport
    {
        public class BusinessConversionReportEnvelope
        {
            public List<BusinessConversionDataDto> BusinessConversionLeads { get; set; }
            public FiscalYear FiscalYear { get; set; }
        }
        public class BusinessConversionQuery : IRequest<BusinessConversionReportEnvelope>
        {
            public int FiscalYearId { get; set; }
        }

        public class Handler : IRequestHandler<BusinessConversionQuery, BusinessConversionReportEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, UserManager<AppUser> userManager, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _userManager = userManager;
                _context = context;
            }
            public async Task<BusinessConversionReportEnvelope> Handle(BusinessConversionQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { message = "Invalid user" });

                var experienceCenterIds = new List<int>();

                var experienceCenters = await _context.ExperienceCenters
                                            .OrderBy(x => x.TeamName).ThenBy(x => x.ExperienceCenterShortName)
                                            .ToListAsync();
                //experienceCenters = experienceCenters.OrderBy(x => x.TeamName).ThenBy(x => x.ExperienceCenterShortName).ToList();

                if (AppUserConstant.ECMANAGER.Equals(_userAccessor.GetCurrentUserRole()))
                {
                    experienceCenterIds = await _context.AppUserExperienceCenters
                                        .Where(x => x.AppUser.Id == user.Id)
                                        .Select(x => x.ExperienceCenter.Id)
                                        .ToListAsync();

                    experienceCenters = await _context.ExperienceCenters
                                            .Where(x => experienceCenterIds.Contains(x.Id))
                                            .OrderBy(x => x.TeamName).ThenBy(x => x.ExperienceCenterShortName)
                                            .ToListAsync();
                }

                if (AppUserConstant.ADMIN.Equals(_userAccessor.GetCurrentUserRole()))
                {
                    experienceCenterIds = await _context.ExperienceCenters.Select(x => x.Id).ToListAsync();
                }
                
                var leadQueryable = _context.Leads
                                    .Where(x => x.LeadStatusId == 5 && experienceCenterIds.Contains(x.AssignedToEC.Id));

                var fiscalYear = await _context.FiscalYears
                                            .Include(x => x.FiscalMonths)
                                            .Where(x => x.Id == request.FiscalYearId)
                                            .FirstOrDefaultAsync();

                leadQueryable = leadQueryable
                             .Where(x => x.LeadConversion >= fiscalYear.StartYearDate && x.LeadConversion <= fiscalYear.EndYearDate); //    .Where(x => x.LastUpdatedAt >= fiscalYear.StartYearDate && x.LastUpdatedAt <= fiscalYear.EndYearDate); //

                var leads = await leadQueryable.ToListAsync();

                List<BusinessConversionDataDto> businessLeadConversions = new List<BusinessConversionDataDto>();

                foreach(var expCenter in experienceCenters)
                {
                    var expCenterWiseConversion = businessLeadConversions
                                            .Where(x => x.ExperienceCenterShortName == expCenter.ExperienceCenterShortName).FirstOrDefault();
                    
                    if (expCenterWiseConversion == null) 
                    {
                        expCenterWiseConversion = new BusinessConversionDataDto();
                        expCenterWiseConversion.ExperienceCenterShortName = expCenter.ExperienceCenterShortName;
                        expCenterWiseConversion.TeamName = expCenter.TeamName;
                        expCenterWiseConversion.LeadConversions = new List<LeadConversionDto>();
                    }

                    foreach(var month in fiscalYear.FiscalMonths)
                    {
                        var leadConversionDto = leads
                                     .Where(x => x.LeadConversion >= month.StartMonthDate && x.LeadConversion <= month.EndMonthDate //.Where(x => x.LastUpdatedAt >= month.StartMonthDate && x.LastUpdatedAt <= month.EndMonthDate // 
                                                && x.AssignedToECId == expCenter.Id)
                                    .GroupBy(x => x.LeadConversion.GetValueOrDefault().Month) //.GroupBy(x => x.LeadConversion.GetValueOrDefault().Month)
                                    .Select(x => new LeadConversionDto
                                    {
                                        ConvertedLeads = x.Count(),
                                        ConversionValue = x.Sum(z => z.LeadValueINR.GetValueOrDefault()),
                                        Month = month.FiscalMonthLabel,
                                        /* ExperienceCenterShortName = expCenter.ExperienceCenterShortName */
                                    }).FirstOrDefault();

                        if (leadConversionDto == null)
                        {
                            leadConversionDto = new LeadConversionDto
                            {
                                Month = month.FiscalMonthLabel,
                                /* ExperienceCenterShortName = expCenter.ExperienceCenterShortName */
                            };
                        }

                        expCenterWiseConversion.LeadConversions.Add(leadConversionDto);   //adding month wise data
                    }

                    var leadConversionSummary = leads
                            .Where(x => x.AssignedToECId == expCenter.Id)
                            .GroupBy(x => x.AssignedToECId)
                            .Select(x => new LeadConversionDto
                            {
                                ConvertedLeads = x.Count(),
                                ConversionValue = x.Sum(z => z.LeadValueINR.GetValueOrDefault()),
                                Month = fiscalYear.FiscalYearDuration,
                            }).FirstOrDefault();

                    if (leadConversionSummary == null) 
                    {
                        leadConversionSummary = new LeadConversionDto
                        {
                            Month = fiscalYear.FiscalYearDuration
                        };
                    }

                    expCenterWiseConversion.LeadConversions.Add(leadConversionSummary);

                    businessLeadConversions.Add(expCenterWiseConversion);          
                }

              

                return new BusinessConversionReportEnvelope
                {
                    BusinessConversionLeads = businessLeadConversions,
                    FiscalYear = fiscalYear
                };
            }
        }
    }
}