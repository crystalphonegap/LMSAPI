using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class LeadConversionReport
    {
        public class LeadConversionReportQuery : IRequest<List<LeadConversionReportDto>>
        {
            public int? ExperienceCenterId { get; set; }
            public int FiscalYearId { get; set; }
        }

        public class Handler : IRequestHandler<LeadConversionReportQuery, List<LeadConversionReportDto>>
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
            public async Task<List<LeadConversionReportDto>> Handle(LeadConversionReportQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { message = "Invalid user" });

                var experienceCenterIds = new List<int>();

                if ("ECManager".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    experienceCenterIds = await _context.AppUserExperienceCenters
                                        .Where(x => x.AppUser.Id == user.Id)
                                        .Select(x => x.ExperienceCenter.Id)
                                        .ToListAsync();

                }

                if (experienceCenterIds.Count == 0 && !"ECManager".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    experienceCenterIds.Add(request.ExperienceCenterId.GetValueOrDefault());
                }

                var leadQueryable = _context.Leads
                                    .Where(x => x.LeadStatusId == 5 && experienceCenterIds.Contains(x.AssignedToEC.Id));

                var fiscalYear = await _context.FiscalYears
                                            .Include(x => x.FiscalMonths)
                                            .Where(x => x.Id == request.FiscalYearId)
                                            .FirstOrDefaultAsync();

                leadQueryable = leadQueryable.Where(x => x.LeadConversion >= fiscalYear.StartYearDate && x.LeadConversion <= fiscalYear.EndYearDate);

                var leads = await leadQueryable.ToListAsync();

                List<LeadConversionReportDto> leadConversions = new List<LeadConversionReportDto>();

                foreach(var month in fiscalYear.FiscalMonths)
                {
                    var leadConversionDto = leads
                                        .Where(x => x.LeadConversion >= month.StartMonthDate && x.LeadConversion <= month.EndMonthDate)
                                        .GroupBy(x => x.LeadConversion.GetValueOrDefault().Month)
                                        .Select(x => new LeadConversionReportDto
                                        {
                                            LeadCount = x.Count(),
                                            LeadValueInINR = x.Sum(z => z.LeadValueINR.GetValueOrDefault()),
                                            Month = month.FiscalMonthLabel,
                                            AverageLeadValue = x.Average(z => z.LeadValueINR.GetValueOrDefault())
                                        }).FirstOrDefault();

                    if (leadConversionDto == null)
                    {
                        leadConversionDto = new LeadConversionReportDto
                        {
                            Month = month.FiscalMonthLabel,
                            LeadCount = 0,
                            LeadValueInINR = 0,
                            AverageLeadValue = 0
                        };
                    }
                    leadConversions.Add(leadConversionDto);
                }

                var leadConversionSummary = leadConversions.Select(x => new LeadConversionReportDto
                {
                    Month = "Grand Total",
                    LeadCount = leadConversions.Sum(x => x.LeadCount),
                    LeadValueInINR = leadConversions.Sum(x => x.LeadValueInINR),
                    AverageLeadValue = leadConversions.Sum(x => x.AverageLeadValue)
                }).FirstOrDefault();

                leadConversions.Add(leadConversionSummary);

                return leadConversions;

            }
        }
    }
}