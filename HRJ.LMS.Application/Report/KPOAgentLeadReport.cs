using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Application.User;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class KPOAgentLeadReport
    {
        public class KPOAgentLeadReportQuery : IRequest<List<KPOAgentLeadDataDto>>
        {
            public DateTime? LeadFromDate { get; set; }
            public DateTime? LeadUptoDate { get; set; }
        }

        public class Handler : IRequestHandler<KPOAgentLeadReportQuery, List<KPOAgentLeadDataDto>>
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
            public async Task<List<KPOAgentLeadDataDto>> Handle(KPOAgentLeadReportQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                //var experienceCenters = await _context.ExperienceCenters.OrderBy(x => x.ExperienceCenterName).ToListAsync();
                var kpoAgents = await _userManager.GetUsersInRoleAsync(AppUserConstant.KPOAGENT);

                kpoAgents = kpoAgents.OrderBy(x => x.FullName).ToList();

                if (_userAccessor.GetCurrentUserRole().Equals(AppUserConstant.KPOAGENT))
                {
                    kpoAgents = kpoAgents.Where(x => x.Id == _userAccessor.GetCurrentUserId()).ToList();
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
                                .Include(x => x.LeadCallingStatus)
                                .ToListAsync();

                List<KPOAgentLeadDataDto> kpoAgentLeads = new List<KPOAgentLeadDataDto>();

                foreach (var kpoAgent in kpoAgents)
                {
                    var userStates = await _context.AppUserStates
                                    .Where(x => x.AppUser == kpoAgent)
                                    .Select(x => x.State.StateName.ToUpper()).ToListAsync();

                    var leadQuery = leads.Where(x => userStates.Contains(x.State?.ToUpper()));
                    //Console.WriteLine(expCenter.ExperienceCenterName);
                    var kpoAgentLeadDataDto = leads.Where(x => userStates.Contains(x.State?.ToUpper()))
                                                .Select(x => new KPOAgentLeadDataDto
                                                {
                                                    KPOAgentName = kpoAgent.FullName,
                                                    LeadAssigned = leadQuery.Count(),
                                                    LostToCompetitions = leadQuery.Count(x => x.LeadCallingStatusId == 1),
                                                    Qualifieds = leadQuery.Count(x => x.LeadCallingStatusId == 2),
                                                    NotReachables = leadQuery.Count(x => x.LeadCallingStatusId == 3),
                                                    NotQualifieds = leadQuery.Count(x => x.LeadCallingStatusId == 4),
                                                    AttendedCalls = leadQuery.Count(x => x.LeadCallingStatusId != null),
                                                    PendingCalls = leadQuery.Count(x => x.LeadCallingStatusId == null),
                                                    Hot = leadQuery.Count(x => x.LeadClassificationId == 3),
                                                    Warm = leadQuery.Count(x => x.LeadClassificationId == 2),
                                                    Cold = leadQuery.Count(x => x.LeadClassificationId == 1)
                                                }).FirstOrDefault();

                    if (kpoAgentLeadDataDto == null)
                    {
                        kpoAgentLeadDataDto = new KPOAgentLeadDataDto
                        {
                            KPOAgentName = kpoAgent.FullName
                        };
                    }
                    kpoAgentLeads.Add(kpoAgentLeadDataDto);
                }

                var leadSummary = kpoAgentLeads.Select(x => new KPOAgentLeadDataDto
                {
                    KPOAgentName = "Grand Total",
                    LeadAssigned = kpoAgentLeads.Sum(x => x.LeadAssigned),
                    LostToCompetitions = kpoAgentLeads.Sum(x => x.LostToCompetitions),
                    Qualifieds = kpoAgentLeads.Sum(x => x.Qualifieds),
                    NotReachables = kpoAgentLeads.Sum(x => x.NotReachables),
                    NotQualifieds = kpoAgentLeads.Sum(x => x.NotQualifieds),
                    AttendedCalls = kpoAgentLeads.Sum(x => x.AttendedCalls),
                    PendingCalls = kpoAgentLeads.Sum(x => x.PendingCalls),
                    Hot = kpoAgentLeads.Sum(x => x.Hot),
                    Warm = kpoAgentLeads.Sum(x => x.Warm),
                    Cold = kpoAgentLeads.Sum(x => x.Cold)
                }).FirstOrDefault();

                kpoAgentLeads.Add(leadSummary);

                return kpoAgentLeads;
            }
        }
    }
}