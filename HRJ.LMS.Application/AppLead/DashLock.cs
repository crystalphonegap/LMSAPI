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
namespace HRJ.LMS.Application.AppLead
{
  public class DashLock
    {

        public class LeadDashlockCommand : IRequest<BaseDto>
        {
            public string LeadId { get; set; }
            public string LeadType { get; set; }
            public string Prefix { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public DateTime Date { get; set; }
            public string Category { get; set; }
            public string City { get; set; }
            public string Area { get; set; }
            public string BranchArea { get; set; }
            public int DNCMobile { get; set; }
            public int DNCPhone { get; set; }
            public string Company { get; set; }
            public string Pincode { get; set; }
            public string Time { get; set; }
            public string BranchPin { get; set; }
            public string ParentId { get; set; }
        }

        public class Handler : IRequestHandler<LeadDashlockCommand, BaseDto>
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

            public async Task<BaseDto> Handle(LeadDashlockCommand request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var DashLockLead = _mapper.Map<LeadDashlockCommand, LeadDashLock>(request);

                var dbLead = _context.Leads
                                .Where(x => x.LeadSourceId == DashLockLead.LeadId
                                    && x.LeadSource == "Dash Lock")
                                .FirstOrDefault();

                var lead = _mapper.Map<LeadDashlockCommand, Lead>(request);

                var stateCityMapping = _context.StateCityMappings
                                .Where(x => x.City.ToLower().Equals(lead.City.ToLower()))
                                .FirstOrDefault();

                if (stateCityMapping != null)
                {
                    lead.State = stateCityMapping.StateName;
                }

                if (dbLead == null) //if not found then adding lead
                {
                    await _context.Leads.AddAsync(lead);
                }

                await _context.LeadDashLock.AddAsync(DashLockLead);

                var success = await _context.SaveChangesAsync() > 0;

                await _leadActivityLog.AddLeadActivityLog(lead, "System", "Start", "Lead Added in the system", 1, null);

                if (success) return new BaseDto { Message = string.Format("Lead Saved Successfully"), StatusCode = (int)HttpStatusCode.OK };

                throw new RestException(HttpStatusCode.BadRequest, new { message = "Problem saving changes" });
            }
        }
    }
}
