using System;

namespace HRJ.LMS.Application.Dto
{
    public class LeadReminderDto
    {
        public Guid Id { get; set; }
        public DateTime RemindAt { get; set; }
        public string ReminderCreatedBy { get; set; }
        public DateTime ReminderCreatedAt { get; set; }
    }
}