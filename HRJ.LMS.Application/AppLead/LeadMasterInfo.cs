using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class LeadMasterInfo
    {
        public class LeadMasterInfoEnvelope
        {
            public List<StateDto> States { get; set; }
            public List<StateDto> StatesFilter { get; set; }
            public List<LeadCallingStatusDto> LeadCallingStatuses { get; set; }
            public List<LeadClassificationDto> LeadClassifications { get; set; }
            public List<LeadEnquiryTypeDto> LeadEnquiryTypes { get; set; }
            public List<LeadStatusDto> LeadStatuses { get; set; }
            public List<LeadSpaceTypeDto> LeadSpaceTypes { get; set; }
            //public List<ExperienceCenterStateDto> ExperienceCenterStates { get; set; }
            public List<ExperienceCenterDto> ExperienceCenters { get; set; }
            public List<LeadReminderOptionDto> LeadReminderOptions { get; set; }
            public List<LeadSourceDto> LeadSources { get; set; }
        }
        public class LeadMasterInfoQuery : IRequest<LeadMasterInfoEnvelope> { }

        public class Handler : IRequestHandler<LeadMasterInfoQuery, LeadMasterInfoEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }
            public async Task<LeadMasterInfoEnvelope> Handle(LeadMasterInfoQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var userId = _userAccessor.GetCurrentUserId();
                var statesquery =  _context.States.AsQueryable();

                if ("KPOAgent".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var userStates = await _context.AppUserStates
                                        .Where(x => x.AppUser.Id == userId)
                                        .Select(x => x.State.StateName)
                                        .ToListAsync<string>();

                    statesquery = statesquery
                                .Where(x => userStates.Contains(x.StateName));
                }
                else if ("ECManager".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var userStates = await _context.AppUserExperienceCenters
                                        .Where(x => x.AppUser.Id == userId)
                                        .Select(x => x.ExperienceCenter.State)
                                        .ToListAsync();

                    statesquery = statesquery
                                .Where(x => userStates.Contains(x.StateName));
                }


                var statesFilter = await statesquery.OrderBy(x => x.StateName).ToListAsync();
                var states = await _context.States.OrderBy(x => x.StateName).ToListAsync();
                var leadCallingStatuses = await _context.LeadCallingStatuses.OrderBy(x => x.RowOrder).ToListAsync();
                var leadClassifications = await _context.LeadClassifications.OrderBy(x => x.RowOrder).ToListAsync();
                var leadEnquiryTypes = await _context.LeadEnquiryTypes.OrderBy(x => x.RowOrder).ToListAsync();
                var leadStatuses = await _context.LeadStatuses.OrderBy(x => x.RowOrder).ToListAsync();
                var leadSpaceTypes = await _context.LeadSpaceTypes.OrderBy(x => x.RowOrder).ToListAsync();
                var leadReminderOpts = await _context.LeadReminderOptions.OrderBy(x => x.RowOrder).ToListAsync();
                var leadSources = await _context.LeadSources.OrderBy(x => x.RowOrder).ToListAsync();
                /* var experienceCenterStates = await _context.ECStateMappings
                                            .Include(x => x.ExperienceCenter)
                                            .Include(x => x.State)
                                            .ToListAsync(); */
                var experienceCenters = await _context.ExperienceCenters.OrderBy(x => x.ExperienceCenterName).ToListAsync();

                return new LeadMasterInfoEnvelope
                {
                    States = _mapper.Map<List<State>, List<StateDto>>(states),
                    StatesFilter = _mapper.Map<List<State>, List<StateDto>>(statesFilter),
                    LeadCallingStatuses = _mapper.Map<List<LeadCallingStatus>, List<LeadCallingStatusDto>>(leadCallingStatuses),
                    LeadClassifications = _mapper.Map<List<LeadClassification>, List<LeadClassificationDto>>(leadClassifications),
                    LeadEnquiryTypes = _mapper.Map<List<LeadEnquiryType>, List<LeadEnquiryTypeDto>>(leadEnquiryTypes),
                    LeadStatuses = _mapper.Map<List<LeadStatus>, List<LeadStatusDto>>(leadStatuses),
                    LeadSpaceTypes = _mapper.Map<List<LeadSpaceType>, List<LeadSpaceTypeDto>>(leadSpaceTypes),
                    //ExperienceCenterStates = _mapper.Map<List<ECStateMapping>, List<ExperienceCenterStateDto>>(experienceCenterStates)
                    ExperienceCenters = _mapper.Map<List<ExperienceCenter>, List<ExperienceCenterDto>>(experienceCenters),
                    LeadReminderOptions = _mapper.Map<List<LeadReminderOption>, List<LeadReminderOptionDto>>(leadReminderOpts),
                    LeadSources = _mapper.Map<List<LeadSource>, List<LeadSourceDto>>(leadSources)
                };
            }
        }
    }
}