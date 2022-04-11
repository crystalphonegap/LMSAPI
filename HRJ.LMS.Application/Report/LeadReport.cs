using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class LeadReport
    {
        public class LeadReportEnvelope
        {
            public byte[] LeadReportInBytes { get; set; }
            public string ReportFileName { get; set; }
        }
        public class LeadReportQuery : IRequest<LeadReportEnvelope>
        {
            public DateTime? LeadFromDate { get; set; }
            public DateTime? LeadUptoDate { get; set; }

        }

        public class Handler : IRequestHandler<LeadReportQuery, LeadReportEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;
            private readonly IExcelFileAccessor _excelFileAccessor;
            public Handler(AppDbContext context, UserManager<AppUser> userManager, IUserAccessor userAccessor, IExcelFileAccessor excelFileAccessor)
            {
                _excelFileAccessor = excelFileAccessor;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _context = context;
            }
            public async Task<LeadReportEnvelope> Handle(LeadReportQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                if (user == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { message = "Invalid user" });


                var leadQueryable = _context.Leads.AsQueryable();

                if ("KPOAgent".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var userStates = await _context.AppUserStates
                                        .Where(x => x.AppUser.Id == user.Id)
                                        .Select(x => x.State.StateName)
                                        .ToListAsync<string>();

                    leadQueryable = leadQueryable
                                .Where(x => userStates.Contains(x.State));                   
                }
                else if ("ECManager".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    var experienceCenterIds = await _context.AppUserExperienceCenters
                                        .Where(x => x.AppUser.Id == user.Id)
                                        .Select(x => x.ExperienceCenter.Id)
                                        .ToListAsync();

                    leadQueryable = leadQueryable
                                .Where(x => experienceCenterIds.Contains(x.AssignedToEC.Id));
                }

                if (request.LeadFromDate != null && request.LeadUptoDate != null)
                {
                    var tempFromDate = request.LeadFromDate.GetValueOrDefault();
                    request.LeadFromDate = new DateTime(tempFromDate.Year, tempFromDate.Month, tempFromDate.Day, 0, 0, 0);
                    request.LeadUptoDate = request.LeadUptoDate.GetValueOrDefault().AddHours(23).AddMinutes(59).AddSeconds(59);
                }
                else
                {
                    request.LeadFromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    request.LeadUptoDate = DateTime.Now;
                }

                leadQueryable = leadQueryable
                                    .Where(x => x.LeadDateTime >= request.LeadFromDate && x.LeadDateTime <= request.LeadUptoDate);

                var leads = await leadQueryable
                                .Include(x => x.LeadContactDetails)
                                .Include(x => x.LeadClassification)
                                .Include(x => x.LeadCallingStatus)
                                .Include(x => x.LeadEnquiryType)
                                .Include(x => x.LeadSpaceType)
                                .Include(x => x.AssignedToEC)
                                .Include(x => x.LeadStatus)
                                .Include(x => x.LeadCallerRemarks)
                                .Include(x => x.LeadECManagerRemarks)
                                .OrderBy(x => x.LeadDateTime)
                                .ToListAsync();

                var excelTemplate = await _context.UploadExcelTemplates
                                        .Where(x => x.LeadSource == "Download")
                                        .OrderBy(x => x.ColumnOrder)
                                        .ToListAsync();

                return new LeadReportEnvelope
                {
                    LeadReportInBytes = await _excelFileAccessor.WriteLeadToExcelFile(leads, excelTemplate),
                    ReportFileName = string.Format("{0}_{1}.xlsx", "LeadReport", string.Format(DateTime.Now.ToString(), "dd-MM-YYYY_HH:mm:ss"))
                };

            }
        }
    }
}