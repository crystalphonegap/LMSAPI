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
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class UploadLead
    {
        public class UploadLeadCommand : IRequest<BaseDto>
        {
            public IFormFile ExcelFile { get; set; }
        }

        public class Handler : IRequestHandler<UploadLeadCommand, BaseDto>
        {
            private readonly AppDbContext _context;
            private readonly IExcelFileAccessor _excelFileAccessor;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly ILeadActivityLog _leadActivityLog;
            private readonly UserManager<AppUser> _userManager;
            public Handler(AppDbContext context, IExcelFileAccessor excelFileAccessor,
            IMapper mapper, IUserAccessor userAccessor, UserManager<AppUser> userManager, ILeadActivityLog leadActivityLog)
            {
            _leadActivityLog = leadActivityLog;
                _userManager = userManager;
                _userAccessor = userAccessor;
                _mapper = mapper;
                _excelFileAccessor = excelFileAccessor;
                _context = context;
            }

        public async Task<BaseDto> Handle(UploadLeadCommand request, CancellationToken cancellationToken)
        {
            //handler logic goes here
            if (request.ExcelFile.Length > 0)
            {
                /* return new BaseDto
                    {
                        Message = string.Format("{0} File Uploaded Successfully, {1} records Inserted and {2} records Updated", 1, 2, 3),
                        StatusCode = 200
                    }; */
                try
                {
                    var uploadExcelTemplate = await _context.UploadExcelTemplates
                                                    //.GroupBy(x => new { x.LeadSource })
                                                    .ToListAsync();

                    var stateCityMappings = await _context.StateCityMappings
                                                .ToListAsync();

                    var uploadLeadList = await _excelFileAccessor.ReadExcelFile(request.ExcelFile, uploadExcelTemplate, stateCityMappings);

                    if (uploadLeadList.Count == 0)
                    {
                        throw new Exception("File is empty, No records available to upload");
                    }

                    var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                    var leadDates = uploadLeadList.Select(x => x.LeadDateTime);
                    var leadMobileNos = uploadLeadList.Select(x => x.MobileNumber);

                    //get existing leads if any from database
                    var leadContacts = _context.LeadContactDetails
                                    .Where(x => leadMobileNos.Contains(x.MobileNumber)
                                        && leadDates.Contains(x.Lead.LeadDateTime))
                                    .Include(x => x.Lead)
                                    .ToList();

                    //extract excel leads which are not present in database based on mobile no and lead date
                    var uploadInsertLeads = uploadLeadList
                                        .Where(x => !(leadContacts.Select(s => s.MobileNumber).Contains(x.MobileNumber)))
                                        .ToList();

                    //extracting leads which are present but with different lead datetime
                    var uploadInsertLeadsWithDifferentDate = uploadLeadList
                                        .Where(x => (leadContacts.Select(s => s.MobileNumber).Contains(x.MobileNumber))
                                        && !leadContacts.Select(s => s.Lead.LeadDateTime).Contains(x.LeadDateTime))
                                        .ToList();

                    uploadInsertLeads.AddRange(uploadInsertLeadsWithDifferentDate);

                    //Addind new records
                    var insertLeads = _mapper.Map<List<UploadLeadDto>, List<Lead>>(uploadInsertLeads);
                    await _context.Leads.AddRangeAsync(insertLeads);

                    //updating existing records
                    var uploadUpdateLeads = uploadLeadList
                                        .Where(x => leadContacts.Select(s => s.MobileNumber).Contains(x.MobileNumber)
                                        && leadContacts.Select(s => s.Lead.LeadDateTime).Contains(x.LeadDateTime))
                                        .ToList();

                    foreach (var leadContact in leadContacts)
                    {
                        var existingLead = uploadUpdateLeads
                                            .Where(x => x.MobileNumber == leadContact.MobileNumber 
                                                || x.PhoneNumber == leadContact.PhoneNumber
                                                && x.LeadDateTime == leadContact.Lead.LeadDateTime)
                                            .FirstOrDefault();

                        leadContact.Lead.ContactPersonName = leadContact.Lead.ContactPersonName ?? existingLead.ContactPersonName;
                        leadContact.Lead.Description = leadContact.Lead.Description ?? existingLead.Description;
                        leadContact.Lead.City = leadContact.Lead.City ?? existingLead.City;
                        leadContact.Lead.State = leadContact.Lead.State ?? existingLead.State;
                        leadContact.EmailAddress = leadContact.EmailAddress ?? existingLead.EmailAddress;
                        leadContact.Lead.LastUpdatedBy = _userAccessor.GetCurrentUserName();
                        leadContact.Lead.LastUpdatedAt = DateTime.Now;
                    }

                    var remarks = string.Format("{0} File Uploaded Successfully, {1} records Inserted and {2} records Updated",
                                request.ExcelFile.FileName,
                                uploadInsertLeads.Count.ToString(),
                                uploadUpdateLeads.Count.ToString());

                    var uploadExcelLog = new UploadExcelLog()
                    {
                        UploadedAt = DateTime.Now,
                        UploadedBy = user,
                        UploadedRemarks = remarks,
                        ExcelFileName = request.ExcelFile.FileName,
                        UploadedByName = _userAccessor.GetCurrentUserName()
                    };

                    _context.UploadExcelLogs.Add(uploadExcelLog);

                    var success = await _context.SaveChangesAsync() > 0;

                    await _leadActivityLog.AddRangeLeadActivityLog(insertLeads, "System", "Start", "Lead Added in the system");

                    if (success) return new BaseDto
                    {
                        Message = remarks,
                        StatusCode = 200
                    };
                }
                catch (Exception ex)
                {
                    throw new RestException(HttpStatusCode.NotAcceptable, new { message = ex.Message });
                }
            }

            throw new Exception("Problem saving changes");
        }
    }
}
}