using System;

namespace HRJ.LMS.Application.Dto
{
    public class LeadActivityDto
    {
        public string ActionTakenBy { get; set; }
        public DateTime ActionTakenOn { get; set; }
        public string LeadActivityRemarks { get; set; }
        public int IsEventToDisplay { get; set; }
        public string ActivityHeading { get; set; }
    }
}