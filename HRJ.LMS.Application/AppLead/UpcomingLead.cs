using System;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class UpcomingLead
    {
        public class UpcomingLeadListEnvelope
        {
            public List<LeadListViewDto> LeadListViewDto { get; set; }
            public int TotalLeads { get; set; }
        }
        public class UpcomingLeadQuery : IRequest<UpcomingLeadListEnvelope> { }

        public class Handler : IRequestHandler<UpcomingLeadQuery, UpcomingLeadListEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;
            public Handler(AppDbContext context, IUserAccessor userAccessor, IMapper mapper, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _mapper = mapper;
                _userAccessor = userAccessor;
                _context = context;
            }
            public async Task<UpcomingLeadListEnvelope> Handle(UpcomingLeadQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var userId = _userAccessor.GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId);

                //var leadQueryable = _context.Leads.AsQueryable();
                var leadQueryable = _context.LeadReminders
                                        .Where(x => x.IsActive == 1 && x.CreatedBy == user)
                                        //.OrderBy(x => x.RemindAt)
                                        .Include(x => x.Lead);

                if ("KPOAgent".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var userStates = await _context.AppUserStates
                                        .Where(x => x.AppUser.Id == userId)
                                        .Select(x => x.State.StateName)
                                        .ToListAsync<string>();

                    leadQueryable = leadQueryable
                                .Where(x => userStates.Contains(x.Lead.State))
                                .Include(x => x.Lead);
                }
                else if ("ECManager".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var experienceCenterIds = await _context.AppUserExperienceCenters
                                        .Where(x => x.AppUser.Id == userId)
                                        .Select(x => x.ExperienceCenter.Id)
                                        .ToListAsync();

                    leadQueryable = leadQueryable
                                .Where(x => experienceCenterIds.Contains(x.Lead.AssignedToEC.Id))
                                .Include(x => x.Lead);
                }

                var leads = await leadQueryable
                                .Select(r => new LeadListViewDto
                                {
                                    Id = r.Lead.Id,
                                    ContactPersonName = r.Lead.ContactPersonName,
                                    LeadDateTime = r.Lead.LeadDateTime,
                                    EnquiryType = r.Lead.EnquiryType,
                                    LeadClassification = r.Lead.LeadClassification.Classification,
                                    LeadSource = r.Lead.LeadSource,
                                    RemindAt = r.RemindAt,
                                    IsReminderExpired = DateTime.Now > r.RemindAt
                                })
                                .Take(10)
                                .OrderBy(x => x.RemindAt)
                                .ToListAsync();


                var leadIds = leads.Select(x => x.Id).ToList();

                var leadContactdetails = await _context.LeadContactDetails
                                            .Where(x => leadIds.Contains(x.LeadId) && x.ContactType.Equals("Primary"))
                                            .ToListAsync();
                foreach (var lead in leads)
                {
                    var leadContactdetail = leadContactdetails.Where(x => x.LeadId == lead.Id).SingleOrDefault();
                    lead.LeadContactDetail = _mapper.Map<LeadContactDetail, LeadContactDetailDto>(leadContactdetail);
                }

                return new UpcomingLeadListEnvelope
                {
                    LeadListViewDto = leads,
                    TotalLeads = leadQueryable.Count()
                };
            }
        }
    }
}