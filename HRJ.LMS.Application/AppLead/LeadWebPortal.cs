using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class LeadWebPortal
    {
        public class LeadWebPortalCommand : IRequest<BaseDto>
        {
            public string LeadId { get; set; }
            public DateTime Date { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string ContactNumber { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Message { get; set; }
            public string WebSiteSource { get; set; }
            public string EnquiryFor { get; set; }
        }

        public class Handler : IRequestHandler<LeadWebPortalCommand, BaseDto>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILeadActivityLog _leadActivityLog;
            public Handler(AppDbContext context, IMapper mapper, ILeadActivityLog leadActivityLog)
            {
                _leadActivityLog = leadActivityLog;
                _mapper = mapper;
                _context = context;
            }

            public async Task<BaseDto> Handle(LeadWebPortalCommand request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var webLead = _mapper.Map<LeadWebPortalCommand, WebLead>(request);

                var dbLead = _context.Leads
                                .Where(x => x.LeadSourceId == webLead.LeadId
                                    && x.LeadSource == request.WebSiteSource)
                                .FirstOrDefault();

                var lead = _mapper.Map<LeadWebPortalCommand, Lead>(request);

                var isLeadExcluded = await _context.ExcludeLeads.Where(x => x.EnquiryFor.ToLower() == request.EnquiryFor.ToLower()).AnyAsync();

                var stateCityMapping = _context.StateCityMappings
                                .Where(x => x.City.ToLower().Equals(lead.City.ToLower()))
                                .FirstOrDefault();

                if (stateCityMapping != null)
                {
                    lead.State = stateCityMapping.StateName;
                }

                //request.EnquiryFor

                if (dbLead == null && !isLeadExcluded) //if not found then adding lead and lead exclusion
                {
                    await _context.Leads.AddAsync(lead);
                    await _leadActivityLog.AddLeadActivityLog(lead, "System", "Start", "Lead Added in the system", 1, null);
                }

                await _context.WebLeads.AddAsync(webLead); //adding lead to log table

                var success = await _context.SaveChangesAsync() > 0;


                if (success) return new BaseDto { Message = string.Format("Lead Saved Successfully"), StatusCode = (int)HttpStatusCode.OK };

                throw new RestException(HttpStatusCode.BadRequest, new { message = "Problem saving changes" });
            }
        }
    }
}