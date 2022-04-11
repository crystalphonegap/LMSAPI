using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Application.User;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class ECManagerLeadReport
    {
        public class ECManagerLeadReportQuery : IRequest<List<ECManagerLeadDataDto>>
        {
            public DateTime? LeadFromDate { get; set; }
            public DateTime? LeadUptoDate { get; set; }
        }

        public class Handler : IRequestHandler<ECManagerLeadReportQuery, List<ECManagerLeadDataDto>>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }
            public async Task<List<ECManagerLeadDataDto>> Handle(ECManagerLeadReportQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var experienceCenters = await _context.ExperienceCenters.OrderBy(x => x.ExperienceCenterShortName).ToListAsync();

                if (_userAccessor.GetCurrentUserRole().Equals(AppUserConstant.ECMANAGER))
                {
                    var expCenterIds = await _context.AppUserExperienceCenters
                                    .Where(x => x.AppUser.Id == _userAccessor.GetCurrentUserId())
                                    .Select(x => x.ExperienceCenter.Id)
                                    .ToListAsync();

                    experienceCenters = experienceCenters.Where(x => expCenterIds.Contains(x.Id)).ToList();
                }

                var leadQueryable = _context.Leads.AsQueryable();

                if (request.LeadFromDate != null && request.LeadUptoDate != null)
                {
                    var tempFromDate = request.LeadFromDate.GetValueOrDefault();
                    request.LeadFromDate = new DateTime(tempFromDate.Year, tempFromDate.Month, tempFromDate.Day, 0, 0, 0);
                    request.LeadUptoDate = request.LeadUptoDate.GetValueOrDefault().AddHours(23).AddMinutes(59).AddSeconds(59);
                    leadQueryable = leadQueryable
                                    .Where(x => x.LeadDateTime >= request.LeadFromDate && x.LeadDateTime <= request.LeadUptoDate);
                }

                var leads = await leadQueryable
                                .Include(x => x.LeadClassification)
                                .Include(x => x.LeadStatus)
                                .ToListAsync();

                List<ECManagerLeadDataDto> ecManagerLeads = new List<ECManagerLeadDataDto>();

                foreach (var expCenter in experienceCenters)
                {
                    var leadQuery = leads.Where(x => x.AssignedToEC == expCenter);
                    //Console.WriteLine(expCenter.ExperienceCenterName);
                    var ecManagerLeadDataDto = leads.Where(x => x.AssignedToEC == expCenter)
                                                .Select(x => new ECManagerLeadDataDto
                                                {
                                                    ExperienceCenterName = expCenter.ExperienceCenterShortName,
                                                    LeadAssigned = leadQuery.Count(),
                                                    NotReachables = leadQuery.Count(x => x.LeadStatusId == 1),
                                                    FollowUps = leadQuery.Count(x => x.LeadStatusId == 2),
                                                    NoRequirements = leadQuery.Count(x => x.LeadStatusId == 3),
                                                    ClosedWithOtherBrands = leadQuery.Count(x => x.LeadStatusId == 4),
                                                    ConvertedLeads = leadQuery.Count(x => x.LeadStatusId == 5),
                                                    AttendedCalls = leadQuery.Count(x => x.LeadStatusId != null),
                                                    PendingCalls = leadQuery.Count(x => x.LeadStatusId == null),
                                                    Hot = leadQuery.Count(x => x.LeadClassificationId == 3),
                                                    Warm = leadQuery.Count(x => x.LeadClassificationId == 2),
                                                    Cold = leadQuery.Count(x => x.LeadClassificationId == 1)
                                                }).FirstOrDefault();

                    if (ecManagerLeadDataDto == null)
                    {
                        ecManagerLeadDataDto = new ECManagerLeadDataDto
                        {
                            ExperienceCenterName = expCenter.ExperienceCenterShortName
                        };
                    }
                    ecManagerLeads.Add(ecManagerLeadDataDto);
                }

                var leadSummary = ecManagerLeads.Select(x => new ECManagerLeadDataDto
                {
                    ExperienceCenterName = "Grand Total",
                    LeadAssigned = ecManagerLeads.Sum(x => x.LeadAssigned),
                    NotReachables = ecManagerLeads.Sum(x => x.NotReachables),
                    FollowUps = ecManagerLeads.Sum(x => x.FollowUps),
                    NoRequirements = ecManagerLeads.Sum(x => x.NoRequirements),
                    ClosedWithOtherBrands = ecManagerLeads.Sum(x => x.ClosedWithOtherBrands),
                    ConvertedLeads = ecManagerLeads.Sum(x => x.ConvertedLeads),
                    AttendedCalls = ecManagerLeads.Sum(x => x.AttendedCalls),
                    PendingCalls = ecManagerLeads.Sum(x => x.PendingCalls),
                    Hot = ecManagerLeads.Sum(x => x.Hot),
                    Warm = ecManagerLeads.Sum(x => x.Warm),
                    Cold = ecManagerLeads.Sum(x => x.Cold)
                }).FirstOrDefault();

                ecManagerLeads.Add(leadSummary);

                return ecManagerLeads;
            }
        }
    }
}