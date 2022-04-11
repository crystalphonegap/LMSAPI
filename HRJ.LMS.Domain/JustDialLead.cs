using System;

namespace HRJ.LMS.Domain
{
    public class JustDialLead
    {
        public Guid Id { get; set; }
        public string LeadId { get; set; }
        public string LeadType { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string BranchArea { get; set; }
        public int DNCMobile { get; set; }
        public int DNCPhone { get; set; }
        public string Company { get; set; }
        public string Pincode { get; set; }
        public string Time { get; set; }
        public string BranchPin { get; set; }
        public string ParentId { get; set; }
        public DateTime LeadCreatedAt { get; set; }
    }
}