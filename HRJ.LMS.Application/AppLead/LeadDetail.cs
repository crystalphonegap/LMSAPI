using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Extensions;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class LeadDetail
    {
        public class LeadDetailQuery : IRequest<LeadDetailDto>
        {
            public Guid LeadId { get; set; }
        }

        public class Handler : IRequestHandler<LeadDetailQuery, LeadDetailDto>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            public Handler(AppDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<LeadDetailDto> Handle(LeadDetailQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var leadDetail = await _context.Leads
                                    .Where(x => x.Id.Equals(request.LeadId))
                                    .Include(x => x.LeadContactDetails)
                                    .Include(x => x.LeadClassification)
                                    .Include(x => x.LeadCallingStatus)
                                    .Include(x => x.LeadEnquiryType)
                                    .Include(x => x.LeadSpaceType)
                                    .Include(x => x.AssignedToEC)
                                    .Include(x => x.LeadStatus)
                                    .Include(x => x.LeadCallerRemarks)
                                    .Include(x => x.LeadECManagerRemarks)
                                    .Include(x => x.LeadActivities)
                                    .Include(x => x.LeadInvoiceFileDetails)
                                    .SingleOrDefaultAsync();

                if (leadDetail == null)
                    throw new RestException(HttpStatusCode.NotFound, new { message = "Lead Detail not found" });

                /*---------------lead reminder logic-------------------*/
                var reminders = await _context.LeadReminders
                                    .Where(x => x.Lead == leadDetail && x.IsActive == 1)
                                    .ToListAsync();

                var ecLeadReminder = reminders
                                    .Where(x => x.UserRole == "ECManager")
                                    .FirstOrDefault();
                var callerLeadReminder = reminders
                                    .Where(x => x.UserRole == "KPOAgent")
                                    .FirstOrDefault();
                /*---------------lead reminder logic-------------------*/

                
                var existingLeadIds = await _context.LeadContactDetails.Where(x => 
                                leadDetail.LeadContactDetails.Select(s => s.MobileNumber).Contains(x.MobileNumber) && x.LeadId != leadDetail.Id && x.MobileNumber != null)
                                .Select(x => x.LeadId)
                                .ToListAsync();

                var leadDetailDto = _mapper.Map<Lead, LeadDetailDto>(leadDetail);

                leadDetailDto.CallerLeadReminder = _mapper.Map<LeadReminder, LeadReminderDto>(callerLeadReminder);
                leadDetailDto.ECLeadReminder = _mapper.Map<LeadReminder, LeadReminderDto>(ecLeadReminder);
                leadDetailDto.ExistingLeadIds = existingLeadIds;

                leadDetailDto.LeadCallerRemarks = leadDetailDto.LeadCallerRemarks.OrderByDescending(x => x.CallerRemarkAt).ToList();
                leadDetailDto.LeadECManagerRemarks = leadDetailDto.LeadECManagerRemarks.OrderByDescending(x => x.ECManagerRemarkAt).ToList();
                leadDetailDto.LeadActivities = leadDetailDto.LeadActivities.OrderByDescending(x => x.ActionTakenOn).ToList();

                SetLeadStatus(leadDetail, leadDetailDto);

                var deletedInvoiceFiles = leadDetailDto.LeadInvoiceFileDetails.Where(x => x.IsActive == false).ToList();
                if (deletedInvoiceFiles != null)
                {
                    foreach(var deletedInvoice in deletedInvoiceFiles)
                    {
                        leadDetailDto.LeadInvoiceFileDetails.Remove(deletedInvoice);
                    }
                }
                
                return leadDetailDto;
            }

            //private void SetLeadStatus(Lead lead, LeadDetailDto leadDetailDto)
            //{
            //    leadDetailDto.OverallLeadStatus = "Open";

            //    if (LeadStatusCode.CallingStatus_NotReachable
            //                .Equals(lead.LeadCallingStatus?.CallingStatus, StringComparison.OrdinalIgnoreCase) 
            //            || lead.LeadStatus == null
            //            || LeadStatusCode.LeadStatus_Follow_up
            //                    .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase))
            //    {
            //        leadDetailDto.OverallLeadStatus = "Open";
            //    }

            //    if (LeadStatusCode.CallingStatus_NotQualified
            //            .Equals(lead.LeadCallingStatus?.CallingStatus, StringComparison.OrdinalIgnoreCase)
            //        || LeadStatusCode.CallingStatus_Lost_To_Competition
            //            .Equals(lead.LeadCallingStatus?.CallingStatus, StringComparison.OrdinalIgnoreCase)
            //        || LeadStatusCode.LeadStatus_Closed_with_HRJ
            //            .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase)
            //        || LeadStatusCode.LeadStatus_Closed_with_other_brand
            //            .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase)
            //        || LeadStatusCode.LeadStatus_No_Requirement
            //            .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase))
            //    {
            //        leadDetailDto.OverallLeadStatus = "Closed";
            //    }
            //}

            private void SetLeadStatus(Lead lead, LeadDetailDto leadDetailDto)
            {

                if (lead.LeadCallingStatusId == 2 || lead.LeadCallingStatusId == 3 || lead.LeadCallingStatusId == null || lead.LeadStatusId == 4 || lead.LeadStatusId == null)
                {
                    leadDetailDto.OverallLeadStatus = "Open";
                }
                else
                {
                    leadDetailDto.OverallLeadStatus = "Close";
                }
              




            }
        }

        
    }
}