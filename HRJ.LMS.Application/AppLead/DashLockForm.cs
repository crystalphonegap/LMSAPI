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
   public class DashLockForm
    {
        public class LeadDashLockFormCommand : IRequest<BaseDto>
        {
            public int Leadsource_Id { get; set; }
            public string location_id { get; set; }//4

            public string location_external_id { get; set; }//5

            public string location_name { get; set; }//6

            public string locality { get; set; }//7

            public string city { get; set; }//8

            public string state { get; set; }//9

            public string pincode { get; set; }//10

            public string lead_date { get; set; }//13
            public string type { get; set; }//14

            public string name { get; set; } //1

            public string email { get; set; }//2

            public string mobile { get; set; }//3

            public string description { get; set; }//11

            public string lead_type { get; set; }//12
        }

        public class Handler : IRequestHandler<LeadDashLockFormCommand, BaseDto>
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

            public async Task<BaseDto> Handle(LeadDashLockFormCommand request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var DashLockLead = _mapper.Map<LeadDashLockFormCommand, LeadDashLock_Form>(request);

                var dbLead = _context.Leads
                                .Where(x => x.LeadSourceId == DashLockLead.Leadsource_Id.ToString()
                                    && x.LeadSource == "Dash Lock")
                                .FirstOrDefault();

                var lead = _mapper.Map<LeadDashLockFormCommand, Lead>(request);

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

                await _context.LeadDashLock_Form.AddAsync(DashLockLead);
                try
                {
                    var success = await _context.SaveChangesAsync() > 0;
                    await _leadActivityLog.AddLeadActivityLog(lead, "System", "Start", "Lead Added in the system", 1, null);

                    if (success) return new BaseDto { Message = string.Format("Lead Saved Successfully"), StatusCode = (int)HttpStatusCode.OK };

                    throw new RestException(HttpStatusCode.BadRequest, new { message = "Problem saving changes" });
                }
                catch(Exception ex)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { message = "Problem saving changes" });
                }

                
            }
        }
    }
}
