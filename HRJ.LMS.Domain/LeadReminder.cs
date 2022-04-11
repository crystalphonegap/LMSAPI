using System;

namespace HRJ.LMS.Domain
{
    public class LeadReminder
    {
        public Guid Id { get; set; }
        public Lead Lead { get; set; }
        public DateTime RemindAt { get; set; }
        public string UserRole { get; set; }
        public int IsActive { get; set; }
        public string ReminderCreatedBy { get; set; }
        public DateTime ReminderCreatedAt { get; set; }
        public string ReminderUpdatedBy { get; set; }
        public DateTime? ReminderUpdatedAt { get; set; }
        public AppUser CreatedBy { get; set; }
    }
}