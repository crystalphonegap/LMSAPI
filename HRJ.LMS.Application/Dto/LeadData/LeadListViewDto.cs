using System;
using System.Collections.Generic;

namespace HRJ.LMS.Application.Dto
{
    public class LeadListViewDto
    {
        public Guid Id { get; set; }
        public string ContactPersonName { get; set; }
        public DateTime LeadDateTime { get; set; }
        public string EnquiryType { get; set; }
        public LeadContactDetailDto LeadContactDetail { get; set; }
        public string LeadClassification { get; set; }
        public string LeadSource { get; set; }
        public DateTime RemindAt { get; set; }
        public int IsLeadUpdated { get; set; }
        public int LeadHistoryCount { get; set; }
        public List<Guid> LeadHistoryIds { get; set; }
        public bool IsReminderExpired { get; set; }
    }
}