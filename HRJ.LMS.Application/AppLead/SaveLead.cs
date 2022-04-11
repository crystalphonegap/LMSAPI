using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Application.User;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class SaveLead
    {
        public class SaveLeadCommand : IRequest<BaseDto>
        {
            public Guid Id { get; set; }
            public string EnquiryType { get; set; }
            public string ContactPersonName { get; set; }
            public List<LeadContactDetailDto> LeadContactDetails { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Company { get; set; }
            public string Subject { get; set; }
            public string Description { get; set; }
            public int? LeadCallingStatusId { get; set; }
            /* public AppUser CalledById { get; set; } */
            public string CallerRemarks { get; set; }
            public int? LeadClassificationId { get; set; }
            public int? LeadStatusId { get; set; }
            public decimal? QuantityInSquareFeet { get; set; }
            public int? LeadEnquiryTypeId { get; set; }
            public int? LeadSpaceTypeId { get; set; }
            public int? AssignedToECId { get; set; }
            public string ECRemarks { get; set; }
            /* public AppUser AssignedToSalesPerson { get; set; } */
            /*  public string SalesPersonRemarks { get; set; } */
            public DateTime? LeadConversion { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public decimal? LeadValueINR { get; set; }
            public decimal? VolumeInSquareFeet { get; set; }
            public string FutureRequirement { get; set; }
            public string FutureRequirementTileSegment { get; set; }
            public string FutureRequirementVolume { get; set; }
            public DateTime? CallerRemindAt { get; set; }
            public DateTime? ECRemindAt { get; set; }
            public bool IsCallerReminderOn { get; set; }
            public bool IsECReminderOn { get; set; }
            public IFormFile InvoiceFile { get; set; }
        }

        public class Handler : IRequestHandler<SaveLeadCommand, BaseDto>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly ILeadActivityLog _leadActivityLog;
            public Handler(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper,
                IUserAccessor userAccessor, ILeadActivityLog leadActivityLog)
            {
                _leadActivityLog = leadActivityLog;
                _userAccessor = userAccessor;
                _mapper = mapper;
                _userManager = userManager;
                _context = context;
            }

            public async Task<BaseDto> Handle(SaveLeadCommand request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { message = "Invalid user" });

                var leadDetail = await _context.Leads
                                    .Include(x => x.AssignedToEC)
                                    .Include(x => x.LeadCallingStatus)
                                    .Include(x => x.LeadStatus)
                                    .Include(x => x.LeadContactDetails)
                                    .Include(x => x.LeadECManagerRemarks)
                                    .Include(x => x.LeadCallerRemarks)
                                    .Where(x => x.Id == request.Id)
                                    .FirstOrDefaultAsync();

                if (leadDetail == null)
                    throw new RestException(HttpStatusCode.NotFound, new { message = "Lead not found to update" });


                if (leadDetail.LeadCallingStatus?.Id != request.LeadCallingStatusId && request.LeadCallingStatusId > 0)
                {
                    leadDetail.LeadCallingStatus = await _context.LeadCallingStatuses.FindAsync(request.LeadCallingStatusId);
                    await _leadActivityLog.AddLeadActivityLog(leadDetail, user.FullName, "Calling Status", 
                        "Calling Status: " + leadDetail.LeadCallingStatus.CallingStatus, 1, user);
                }

                if (leadDetail.State != request.State && !string.IsNullOrEmpty(request.State))
                {
                    await _leadActivityLog.AddLeadActivityLog(leadDetail, user.FullName, "State", 
                        string.Format("State Changed: from {0} to {1}", leadDetail.State, request.State), 1, user);
                    leadDetail.State = request.State ?? leadDetail.State;
                }

                if (leadDetail.AssignedToEC?.Id != request.AssignedToECId && request.AssignedToECId > 0)
                {
                    leadDetail.AssignedToEC = await _context.ExperienceCenters.FindAsync(request.AssignedToECId);
                    await _leadActivityLog.AddLeadActivityLog(leadDetail, user.FullName, "Assignment",
                            "Assigned to EC: " + leadDetail.AssignedToEC.ExperienceCenterName, 1, user);
                    leadDetail.AssignedAtToEC = DateTime.Now;
                }
                
                leadDetail.LeadClassification = request.LeadClassificationId > 0 ? await _context.LeadClassifications.FindAsync(request.LeadClassificationId) : leadDetail.LeadClassification;
                leadDetail.LeadEnquiryType = request.LeadEnquiryTypeId > 0 ? await _context.LeadEnquiryTypes.FindAsync(request.LeadEnquiryTypeId) : leadDetail.LeadEnquiryType;
                leadDetail.LeadSpaceType = request.LeadSpaceTypeId > 0 ? await _context.LeadSpaceTypes.FindAsync(request.LeadSpaceTypeId) : leadDetail.LeadSpaceType;

                
                //leadDetail.LeadStatus = request.LeadStatusId > 0 ? await _context.LeadStatuses.FindAsync(request.LeadStatusId) : leadDetail.LeadStatus;

                if (!string.IsNullOrEmpty(request.ContactPersonName))
                {
                    leadDetail.ContactPersonName = request.ContactPersonName;
                }

                //leadDetail.CallerRemarks = request.CallerRemarks ?? leadDetail.CallerRemarks;
                if (!string.IsNullOrEmpty(request.CallerRemarks))
                {
                    if (leadDetail.LeadCallerRemarks == null)
                    {
                        leadDetail.LeadCallerRemarks = new List<LeadCallerRemark>();
                    }
                    leadDetail.LeadCallerRemarks.Add(
                        new LeadCallerRemark()
                        {
                            CallingStatus = leadDetail.LeadCallingStatus?.CallingStatus,
                            CallerRemark = request.CallerRemarks,
                            CallerRemarkAt = DateTime.Now,
                            CallerRemarkBy = _userAccessor.GetCurrentUserName()
                        }
                    );
                }

                

                foreach(var contactDetail in request.LeadContactDetails)
                {
                    var dbContactDetail = leadDetail.LeadContactDetails.Where(x => x.ContactType == contactDetail.ContactType).SingleOrDefault();

                    if (dbContactDetail == null)
                    {
                        var newContactDetail = _mapper.Map<LeadContactDetailDto, LeadContactDetail>(contactDetail);
                        leadDetail.LeadContactDetails.Add(newContactDetail);
                    } else 
                    {
                        if (string.IsNullOrEmpty(contactDetail.MobileNumber) && string.IsNullOrEmpty(contactDetail.EmailAddress))
                        {
                            leadDetail.LeadContactDetails.Remove(dbContactDetail);
                        }
                        else
                        {
                            dbContactDetail.MobileNumber = contactDetail.MobileNumber;
                            dbContactDetail.EmailAddress = contactDetail.EmailAddress;
                            dbContactDetail.ContactType = contactDetail.ContactType;
                        }
                        
                    }
                }


                leadDetail.City = request.City ?? leadDetail.City;
                leadDetail.Address = request.Address ?? leadDetail.Address;
                leadDetail.Company = request.Company ?? leadDetail.Company;

                leadDetail.QuantityInSquareFeet = request.QuantityInSquareFeet;
                leadDetail.LeadValueINR = request.LeadValueINR;
                if (request.LeadConversion.HasValue) 
                {
                    leadDetail.LeadConversion = TimeZoneInfo.ConvertTimeFromUtc(request.LeadConversion.GetValueOrDefault(), TimeZoneInfo.Local);
                }
                
                leadDetail.DealerCode = string.IsNullOrWhiteSpace(request.DealerCode) ? null : request.DealerCode.Trim();
                leadDetail.DealerName = string.IsNullOrWhiteSpace(request.DealerName) ? null : request.DealerName.Trim();
                leadDetail.VolumeInSquareFeet = request.VolumeInSquareFeet;
                leadDetail.FutureRequirement = string.IsNullOrWhiteSpace(request.FutureRequirement) ? null : request.FutureRequirement.Trim();
                leadDetail.FutureRequirementVolume = string.IsNullOrWhiteSpace(request.FutureRequirementVolume) ? null : request.FutureRequirementVolume.Trim();
                leadDetail.FutureRequirementTileSegment = string.IsNullOrWhiteSpace(request.FutureRequirementTileSegment) ? null : request.FutureRequirementTileSegment.Trim();

                if (leadDetail.LeadStatus?.Id != request.LeadStatusId && request.LeadStatusId > 0)
                {
                    leadDetail.LeadStatus = await _context.LeadStatuses.FindAsync(request.LeadStatusId);
                    await _leadActivityLog.AddLeadActivityLog(leadDetail, user.FullName, "EC Status",
                                "EC Status: " + leadDetail.LeadStatus.Status, 1, user);
                    leadDetail.AssignedAtToEC = DateTime.Now;

                    if (request.LeadStatusId != 5)
                    {
                        request.LeadConversion = null;
                        leadDetail.LeadConversion = null;
                        leadDetail.DealerCode = null;
                        leadDetail.DealerName = null;
                        leadDetail.LeadValueINR = null;
                        leadDetail.VolumeInSquareFeet = null;
                        leadDetail.FutureRequirement = null;
                        leadDetail.FutureRequirementTileSegment = null;
                        leadDetail.FutureRequirementVolume = null;

                        var invoicefiles = await _context.LeadInvoiceFileDetails
                                .Where(x => x.Lead == leadDetail && x.IsActive == true).ToListAsync();

                        foreach(var invoicefile in invoicefiles)
                        {
                            invoicefile.IsActive = false;
                            invoicefile.RemovedBy = user;
                            invoicefile.RemovedAt = DateTime.Now;
                        }

                    }
                }

                if (!string.IsNullOrEmpty(request.ECRemarks))
                {
                    if (leadDetail.LeadECManagerRemarks == null)
                    {
                        leadDetail.LeadECManagerRemarks = new List<LeadECManagerRemark>();
                    }

                    leadDetail.LeadECManagerRemarks.Add
                    (
                        new LeadECManagerRemark()
                        {
                            LeadStatus = leadDetail.LeadStatus.Status,
                            ECManagerRemark = request.ECRemarks,
                            ECManagerRemarkAt = DateTime.Now,
                            ECManagerRemarkBy = _userAccessor.GetCurrentUserName()
                        }
                    );
                }

                leadDetail.LastUpdatedAt = DateTime.Now;
                leadDetail.LastUpdatedBy = _userAccessor.GetCurrentUserName();

                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains(AppUserConstant.KPOAGENT))
                {
                    leadDetail.IsLeadUpdated = 1;
                }
                else if (roles.Contains(AppUserConstant.ECMANAGER))
                {
                    leadDetail.IsLeadUpdated = 2;
                }

                await SetReminder(leadDetail, request.IsCallerReminderOn, request.CallerRemindAt, user);
                await SetReminder(leadDetail, request.IsECReminderOn, request.ECRemindAt, user);

                try
                {
                    /* Console.WriteLine(request.TruckNo); */
                    var success = await _context.SaveChangesAsync() > 0;
                    if (success) return new BaseDto { Message = string.Format("Lead Info Saved Successfully"), StatusCode = (int)HttpStatusCode.OK };
                }
                catch (Exception ex)
                {
                    throw new RestException(HttpStatusCode.BadRequest,
                        new
                        {
                            message = string.Format("Kindly check and try again"),
                            errorMessage = ex.InnerException.Message
                        });
                }
                throw new RestException(HttpStatusCode.BadRequest, new { message = "Problem saving changes" });
            }

            private async Task SetReminder(Lead leadDetail, Boolean isReminderOn, DateTime? remindAt, AppUser user)
            {
                if (remindAt.HasValue && isReminderOn)
                {
                    var leadReminders = await _context.LeadReminders
                                            .Where(x => x.Lead.Id == leadDetail.Id)
                                            .ToListAsync();

                    foreach (var reminder in leadReminders)
                    {
                        reminder.IsActive = 0;  //reminder off
                        reminder.ReminderUpdatedAt = DateTime.Now;
                        reminder.ReminderUpdatedBy = _userAccessor.GetCurrentUserName();
                    }

                    var leadReminder = new LeadReminder()
                    {
                        Lead = leadDetail,
                        IsActive = 1,   //reminder on
                        ReminderCreatedAt = DateTime.Now,
                        ReminderCreatedBy = _userAccessor.GetCurrentUserName(),
                        UserRole = _userAccessor.GetCurrentUserRole(),
                        RemindAt = new DateTimeOffset(remindAt.GetValueOrDefault()).LocalDateTime,
                        CreatedBy = user
                    };

                    _context.LeadReminders.Add(leadReminder);
                }
                else if (isReminderOn == false)
                {
                    var leadReminders = await _context.LeadReminders
                                            .Where(x => x.Lead.Id == leadDetail.Id)
                                            .ToListAsync();

                    foreach (var reminder in leadReminders)
                    {
                        reminder.IsActive = 0;  //reminder off
                        reminder.ReminderUpdatedAt = DateTime.Now;
                        reminder.ReminderUpdatedBy = _userAccessor.GetCurrentUserName();
                    }
                }
            }
        }
    }
}