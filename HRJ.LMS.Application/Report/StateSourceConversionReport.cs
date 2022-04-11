using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class StateSourceConversionReport
    {
        public class StateSourceConversionReportQuery : IRequest<List<StateSourceConversionDto>>
        {
            public DateTime? LeadFromDate { get; set; }
            public DateTime? LeadUptoDate { get; set; }
        }

        public class Handler : IRequestHandler<StateSourceConversionReportQuery, List<StateSourceConversionDto>>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }
            public async Task<List<StateSourceConversionDto>> Handle(StateSourceConversionReportQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var states = await _context.States.OrderBy(x => x.StateName).ToListAsync();
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
                                .Include(x => x.LeadStatus)
                                .OrderBy(x => x.State)
                                .ToListAsync();

                List<StateSourceConversionDto> stateSourceConversions = new List<StateSourceConversionDto>();

                var leadvalues = leads.GroupBy(x => x.State)
                                        .Select(x => new {
                                                LeadValueINR = x.Sum(x => x.LeadValueINR),
                                                State = x.Key
                                            });

                foreach(var state in states)
                {
                    var leadQuery = leads.Where(x => x.State == state.StateName)
                                    .GroupBy(x => x.LeadSource)
                                    .Select(x => new StateSourceConversionDto {
                                        LeadCount = x.Count(),
                                        QualifiedLeads = x.Count(x => x.LeadCallingStatusId == 2),
                                        //AssignedLeads = x.Count(x => x.AssignedToECId != null && x.LeadCallingStatusId == 2),
                                        ConvertedLeads = x.Count(x => x.LeadStatusId == 5),
                                        ConvertedLeadValue = x.Sum(x => x.LeadValueINR.GetValueOrDefault()),
                                        StateName = state.StateName,
                                        LeadSource = x.Key,
                                        ContributionConversion = GetContribution(x.Sum(x => x.LeadValueINR.GetValueOrDefault()), state.StateName, leadvalues)
                                    }).ToList();

                    if (leadQuery == null || leadQuery.Count() == 0)
                    {
                        leadQuery = new List<StateSourceConversionDto>
                        {
                            new StateSourceConversionDto
                            {
                                StateName = state.StateName
                            }
                        };
                    }

                    stateSourceConversions.AddRange(leadQuery);
                    var summaryStateConversion = stateSourceConversions
                                        .Where(x => x.StateName == state.StateName)
                                        .Select(x => new StateSourceConversionDto {
                                            LeadCount = leadQuery.Sum(x => x.LeadCount),
                                            QualifiedLeads = leadQuery.Sum(x => x.QualifiedLeads),
                                            ConvertedLeads = leadQuery.Sum(x => x.ConvertedLeads),
                                            ConvertedLeadValue = leadQuery.Sum(x => x.ConvertedLeadValue),
                                            StateName = state.StateName,
                                            LeadSource = "Total"
                                        }).FirstOrDefault();

                    stateSourceConversions.Add(summaryStateConversion);
                }
                
                return stateSourceConversions;
            }

            private decimal GetContribution(decimal leadValue, string stateName, IEnumerable<dynamic> leadvalues)
            {
                var summaryLeadValue = leadvalues.Where(x => x.State == stateName).FirstOrDefault().LeadValueINR;

                if (summaryLeadValue == 0)
                {
                    return 0;
                }

                return Convert.ToDecimal(leadValue) / summaryLeadValue;
            }
        }
    }
}