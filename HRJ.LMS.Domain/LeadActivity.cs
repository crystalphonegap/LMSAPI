using System;

namespace HRJ.LMS.Domain
{
    public class LeadActivity
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }
        public string ActionTakenBy { get; set; }
        public DateTime ActionTakenOn { get; set; }
        public string LeadActivityRemarks { get; set; }
        public int IsEventToDisplay { get; set; }
        public string ActivityHeading { get; set; }
        public AppUser AppUser { get; set; }
    }
}