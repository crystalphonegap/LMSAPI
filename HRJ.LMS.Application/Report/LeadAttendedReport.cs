using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Application.User;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class LeadAttendedReport
    {
        public class LeadAttendedReportQuery : IRequest<List<LeadAttendedDto>>
        {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<LeadAttendedReportQuery, List<LeadAttendedDto>>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;
            public Handler(AppDbContext context, IUserAccessor userAccessor, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
                _context = context;
            }
            public async Task<List<LeadAttendedDto>> Handle(LeadAttendedReportQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                if (user == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { message = "Invalid user" });
                
                var tempStartDate = DateTime.Now.AddDays(-7);
                var tempEndDate = DateTime.Now;
                if (request.StartDate.HasValue)
                {
                    tempStartDate = request.StartDate.GetValueOrDefault();
                    tempEndDate = request.EndDate.GetValueOrDefault();
                }

                request.StartDate = new DateTime(tempStartDate.Year, tempStartDate.Month, tempStartDate.Day, 0, 0, 0);
                request.EndDate = new DateTime(tempEndDate.Year, tempEndDate.Month, tempEndDate.Day, 23, 59, 59);
                
                var leadActivity = _context.LeadActivities
                                        .Where(x => x.ActionTakenOn >= request.StartDate.GetValueOrDefault() && 
                                        x.ActionTakenOn <= request.EndDate.GetValueOrDefault())
                                        .AsQueryable();


                if (_userAccessor.GetCurrentUserRole().Equals(AppUserConstant.ADMIN))
                {
                    leadActivity = leadActivity.Where(x => x.AppUser.Id == request.AppUserId).OrderByDescending(x => x.ActionTakenOn);
                }
                else
                {
                    leadActivity = leadActivity.Where(x => x.AppUser == user).OrderByDescending(x => x.ActionTakenOn);
                }

                var leadActivities = await leadActivity.ToListAsync();  //query executed

                var leadAttendedCount = leadActivities.GroupBy(x => x.ActionTakenOn.ToShortDateString())
                                        .Select(x => new LeadAttendedDto
                                        {
                                            LeadAttended = x.Count(),
                                            LeadAttendedAt = x.Key
                                        }).ToList();

                return leadAttendedCount;
            }
        }
    }
}