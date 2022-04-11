using System;

namespace HRJ.LMS.Domain
{
    public class LeadCallerRemark
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }
        public string CallerRemark { get; set; }
        public DateTime CallerRemarkAt { get; set; }
        public string CallerRemarkBy { get; set; }
        public string CallingStatus { get; set; }
    }
}