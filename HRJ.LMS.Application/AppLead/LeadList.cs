using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Extensions;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class LeadList
    {
        public class LeadListEnvelope
        {
            public List<LeadListViewDto> LeadListViewDto { get; set; }
            public int TotalLeads { get; set; }
        }
        public class LeadListQuery : IRequest<LeadListEnvelope>, IQueryObject
        {
            public int? PageNo { get; set; }
            public int? PageSize { get; set; }
            public string SortBy { get; set; }
            public bool IsSortingAscending { get; set; }
            public DateTime? LeadFromDate { get; set; }
            public DateTime? LeadUptoDate { get; set; }
            public int? LeadCallingStatusId { get; set; }
            public string LeadSource { get; set; }
            public int? LeadAttendedId { get; set; }
            public int? LeadClassificationId { get; set; }
            public bool IsUpcomingLeads { get; set; }
            public int? ExperienceCenterId { get; set; }
            public string StateName { get; set; }
            public int? LeadStatusId { get; set; }
            public string ContactPersonName { get; set; }
            public string LeadContactNo { get; set; }
            public int? LeadECStatusId { get; set; }
        }

        public class Handler : IRequestHandler<LeadListQuery, LeadListEnvelope>
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
            public async Task<LeadListEnvelope> Handle(LeadListQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var userId = _userAccessor.GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId);

                var states = await _context.States.Select(x => x.StateName).ToListAsync();
                var leadQueryable = _context.Leads.AsQueryable();
                /* var user = await _userManager.FindByIdAsync(userId);
                var userRoles = await _userManager.GetRolesAsync(user); */
                //ECManager
                //KPOAgent

                if (!string.IsNullOrEmpty (request.ContactPersonName)) 
                {
                    leadQueryable = leadQueryable.
                                        Where(x => x.ContactPersonName.Contains(request.ContactPersonName));
                }

                if (!string.IsNullOrEmpty (request.LeadContactNo)) 
                {
                    var leadIds = _context.LeadContactDetails
                                        .Where(x => x.MobileNumber.Contains(request.LeadContactNo))
                                        .Select(x => x.LeadId)
                                        .ToList();
                    leadQueryable = leadQueryable.Where(x => leadIds.Contains(x.Id));
                }

                var leadUpdated = 1;
                if ("KPOAgent".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var userStates = await _context.AppUserStates
                                        .Where(x => x.AppUser.Id == userId)
                                        .Select(x => x.State.StateName)
                                        .ToListAsync<string>();

                    leadQueryable = leadQueryable
                                .Where(x => userStates.Contains(x.State));
                    
                    leadUpdated = 1;
                }
                else if ("ECManager".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var experienceCenterIds = await _context.AppUserExperienceCenters
                                        .Where(x => x.AppUser.Id == userId)
                                        .Select(x => x.ExperienceCenter.Id)
                                        .ToListAsync();

                    leadQueryable = leadQueryable
                                .Where(x => experienceCenterIds.Contains(x.AssignedToEC.Id));
                    
                    leadUpdated = 2;
                }
                else if (request.ExperienceCenterId.HasValue)
                {
                    leadUpdated = 2;
                }

                if ("Others".Equals(request.StateName)) {
                    leadQueryable = leadQueryable
                                    .Where(x => !states.Contains(x.State) || x.State == null || x.State.Trim() == "");
                }
                else if (!string.IsNullOrEmpty(request.StateName)) {
                    leadQueryable = leadQueryable
                                    .Where(x => x.State == request.StateName);
                }
                

                if (request.LeadFromDate != null && request.LeadUptoDate != null)
                {
                    var tempFromDate = request.LeadFromDate.GetValueOrDefault();
                    request.LeadFromDate = new DateTime(tempFromDate.Year, tempFromDate.Month, tempFromDate.Day, 0, 0, 0);
                    request.LeadUptoDate = request.LeadUptoDate.GetValueOrDefault().AddHours(23).AddMinutes(59).AddSeconds(59);
                    leadQueryable = leadQueryable
                                    .Where(x => x.LeadDateTime >= request.LeadFromDate && x.LeadDateTime <= request.LeadUptoDate);
                }
                else if(!request.IsUpcomingLeads && (string.IsNullOrEmpty(request.ContactPersonName) && string.IsNullOrEmpty(request.LeadContactNo)))
                {
                    var tempFromDate = DateTime.Now.AddDays(-30);
                    request.LeadFromDate = new DateTime(tempFromDate.Year, tempFromDate.Month, tempFromDate.Day, 0, 0, 0);
                    request.LeadUptoDate = DateTime.Now;
                    leadQueryable = leadQueryable
                                    .Where(x => x.LeadDateTime >= request.LeadFromDate && x.LeadDateTime <= request.LeadUptoDate);
                }

                

                if (!string.IsNullOrEmpty(request.LeadSource))
                {
                    leadQueryable = leadQueryable
                                    .Where(x => x.LeadSource == request.LeadSource);
                }

                if (request.LeadAttendedId != null)
                {
                    switch (request.LeadAttendedId.GetValueOrDefault())
                    {
                        case -1:
                            leadQueryable = leadQueryable.Where(x => x.IsLeadUpdated == leadUpdated-1);
                            break;
                        case 1:
                            leadQueryable = leadQueryable.Where(x => x.IsLeadUpdated == leadUpdated);
                            break;
                    }
                }

                if (request.LeadCallingStatusId != null)
                {
                    if (request.LeadCallingStatusId == -1) 
                    {
                        leadQueryable = leadQueryable
                                    .Where(x => x.LeadCallingStatusId == null);
                    }
                    else
                    {
                        leadQueryable = leadQueryable
                                    .Where(x => x.LeadCallingStatus.Id == request.LeadCallingStatusId);
                    }
                }

                if (request.LeadECStatusId != null)
                {
                    if (request.LeadECStatusId == -1) 
                    {
                        leadQueryable = leadQueryable
                                    .Where(x => x.LeadStatusId == null);
                    }
                    else
                    {
                        leadQueryable = leadQueryable
                                    .Where(x => x.LeadStatus.Id == request.LeadECStatusId);
                    }
                    
                }

                if (request.LeadClassificationId != null)
                {
                    leadQueryable = leadQueryable
                                    .Where(x => x.LeadClassification.Id == request.LeadClassificationId);
                }

                if (request.ExperienceCenterId != null)
                {
                    leadQueryable = leadQueryable
                                    .Where(x => x.AssignedToEC.Id == request.ExperienceCenterId);
                }

                if (request.LeadStatusId != null || request.LeadStatusId > 0)
                {
                    switch(request.LeadStatusId)
                    {
                        case 1:
                            leadQueryable = leadQueryable
                                    .Where(x => x.LeadCallingStatusId == 3 || x.LeadCallingStatus == null  //Not Reachable
                                    && (x.LeadStatus == null 
                                    || x.LeadStatusId == 2));     //Follow Up
                        break;

                        case 2:
                            leadQueryable = leadQueryable
                                    .Where(x => x.LeadCallingStatusId == 4  //Not Qualified
                                    || x.LeadCallingStatusId == 1  // Lost To Competition
                                    || x.LeadStatusId == 5   //Closed with HRJ - Converted Lead
                                    || x.LeadStatusId == 4  //Closed with other brands
                                    || x.LeadStatusId == 3);    //No Requirement
                        break;
                    }
                }

                if (request.IsUpcomingLeads) {   
                    var leadReminderIds = _context.LeadReminders
                                        .Where(x => x.IsActive == 1 && x.CreatedBy == user)
                                        .Select(x => x.Lead.Id);

                    leadQueryable = leadQueryable
                                    .Where(x => leadReminderIds.Contains(x.Id));
                }

                var leadSources = new List<string>() 
                {
                    LeadStatusCode.India_Mart,
                    LeadStatusCode.Social_Media
                };

                var leads = await leadQueryable
                                .Select(lead => new LeadListViewDto
                                {
                                    Id = lead.Id,
                                    ContactPersonName = lead.ContactPersonName,
                                    LeadDateTime = lead.LeadDateTime,
                                    EnquiryType = lead.EnquiryType,
                                    LeadContactDetail = _mapper.Map<LeadContactDetail, LeadContactDetailDto>(lead.LeadContactDetails.Where(x => x.ContactType.Equals("Primary")).SingleOrDefault()),
                                    LeadClassification = lead.LeadClassification.Classification,
                                    LeadSource = leadSources.Contains(lead.LeadSource) 
                                                    ? string.Format("{0} - {1}", lead.LeadSource, lead.EnquiryType) 
                                                    : lead.LeadSource,
                                    IsLeadUpdated = lead.IsLeadUpdated
                                })
                                .OrderByDescending(x => x.LeadDateTime)
                                .ApplyPaging(request)
                                .ToListAsync();

                /* var leadMobileNos = leads.Select(x => x.LeadContactDetail.MobileNumber);
                var leadIds = leads.Select(x => x.Id); */
                //finding existing leads based on mobile no.
                
                try
                {
                    var existingLeadContacts = await _context.LeadContactDetails
                                                                           .Where(x => leads.Select(x => x.LeadContactDetail.MobileNumber).Contains(x.MobileNumber) && x.MobileNumber != null)
                                                                           .ToListAsync();

                    foreach (var existingLead in existingLeadContacts)
                    {
                        var leadWithSameContactNos = leads.Where(x => x.LeadContactDetail.MobileNumber != null && x.LeadContactDetail.MobileNumber.Equals(existingLead.MobileNumber));
                        if (leadWithSameContactNos != null)
                        {
                            foreach (var lead in leadWithSameContactNos)
                            {
                                if (existingLead.LeadId != lead.Id)
                                {
                                    lead.LeadHistoryCount += 1;

                                    if (lead.LeadHistoryIds == null)
                                    {
                                        lead.LeadHistoryIds = new List<Guid>();
                                    }
                                    lead.LeadHistoryIds.Add(existingLead.LeadId);
                                }

                            }

                        }
                    }
                }
                catch(Exception)
                {

                }
                    
                
               

        

                return new LeadListEnvelope
                {
                    LeadListViewDto = leads,
                    TotalLeads = leadQueryable.Count()
                };
            }
        }
    }
}