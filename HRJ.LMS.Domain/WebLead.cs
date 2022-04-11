using System;

namespace HRJ.LMS.Domain
{
    public class WebLead
    {
        public Guid Id { get; set; }
        public string LeadId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Message { get; set; }
        public string WebSiteSource { get; set; }
        public DateTime LeadCreatedAt { get; set; }
    }
}