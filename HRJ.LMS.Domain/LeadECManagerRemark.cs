using System;

namespace HRJ.LMS.Domain
{
    public class LeadECManagerRemark
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }
        public string ECManagerRemark { get; set; }
        public DateTime ECManagerRemarkAt { get; set; }
        public string ECManagerRemarkBy { get; set; }
        public string LeadStatus { get; set; }
    }
}