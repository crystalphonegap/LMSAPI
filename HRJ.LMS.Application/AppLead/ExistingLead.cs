using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class ExistingLead
    {
        public class ExistingLeadQuery : IRequest<List<ExistingLeadDto>>
        {
            public List<Guid> ExistingLeadIds { get; set; }
        }

        public class Handler : IRequestHandler<ExistingLeadQuery, List<ExistingLeadDto>>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            public Handler(AppDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<List<ExistingLeadDto>> Handle(ExistingLeadQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here

                var leads = await _context.Leads
                                .Where(x => request.ExistingLeadIds.Contains(x.Id))
                                .Include(x => x.LeadCallingStatus)
                                .Include(x => x.LeadClassification)
                                .Include(x => x.LeadCallerRemarks)
                                .Include(x => x.LeadECManagerRemarks)
                                .Select(x => new ExistingLeadDto
                                {
                                    Id = x.Id,
                                    ContactPersonName = x.ContactPersonName,
                                    LeadDateTime = x.LeadDateTime,
                                    LeadSource = x.LeadSource,
                                    City = x.City,
                                    State = x.State,
                                    Subject = x.Subject,
                                    Description = x.Description,
                                    LeadCallingStatus = x.LeadCallingStatus.CallingStatus,
                                    LeadClassification = x.LeadClassification.Classification,
                                    AssignedToEC = x.AssignedToEC.ExperienceCenterName,
                                    AssignedAtToEC = x.AssignedAtToEC, 
                                    LeadStatus = x.LeadStatus.Status,
                                    LeadCallerRemarks = _mapper.Map<List<LeadCallerRemark>, List<LeadCallerRemarkDto>>(x.LeadCallerRemarks)
                                })
                                .ToListAsync();

                return leads;
            }
        }
    }
}