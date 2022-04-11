using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;

namespace HRJ.LMS.Infrastructure.Utilities
{
    public class LeadActivityLog : ILeadActivityLog
    {
        private readonly AppDbContext _context;
        public LeadActivityLog(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddLeadActivityLog(LeadActivity leadActivity)
        {
            await _context.LeadActivities.AddAsync(leadActivity);
            await _context.SaveChangesAsync();
        }

        public async Task AddLeadActivityLog(Lead lead, string userName, string heading, string leadActivityRemarks, int isEventToDisplay, AppUser appUser)
        {
            var leadActivity = new LeadActivity
            {
                Lead = lead,
                ActionTakenBy = userName,
                ActionTakenOn = DateTime.Now,
                LeadActivityRemarks = leadActivityRemarks,
                ActivityHeading = heading,
                IsEventToDisplay = isEventToDisplay,
                AppUser = appUser
            };

            await _context.LeadActivities.AddAsync(leadActivity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeLeadActivityLog(List<LeadActivity> leadActivities)
        {
            await _context.LeadActivities.AddRangeAsync(leadActivities);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeLeadActivityLog(List<Lead> leads, string userName, string heading, string leadActivityRemarks)
        {
            var leadActivities = new List<LeadActivity>();

            foreach(var lead in leads)
            {
                leadActivities.Add(new LeadActivity 
                {
                    Lead = lead,
                    ActionTakenBy = userName,
                    ActionTakenOn = DateTime.Now,
                    LeadActivityRemarks = leadActivityRemarks,
                    ActivityHeading = heading,
                    IsEventToDisplay = 1
                });
            }

            await _context.LeadActivities.AddRangeAsync(leadActivities);
            await _context.SaveChangesAsync();
        }
    }
}