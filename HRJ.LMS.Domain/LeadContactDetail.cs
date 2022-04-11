using System;

namespace HRJ.LMS.Domain
{
    public class LeadContactDetail
    {
        public Guid Id { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ContactType { get; set; }
        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }
    }
}