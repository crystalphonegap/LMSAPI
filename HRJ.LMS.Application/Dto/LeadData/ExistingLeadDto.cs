using System;
using System.Collections.Generic;

namespace HRJ.LMS.Application.Dto
{
    public class ExistingLeadDto
    {
        public Guid Id { get; set; }
        public DateTime LeadDateTime { get; set; }
        public string ContactPersonName { get; set; }
        /* public string Address { get; set; } */
        public string City { get; set; }
        public string State { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string LeadCallingStatus { get; set; }
        public string LeadClassification { get; set; }
        public List<LeadCallerRemarkDto> LeadCallerRemarks { get; set; }
        /* public List<LeadECManagerRemarkDto> LeadECManagerRemarks { get; set; } */
        public string AssignedToEC { get; set; }
        public DateTime? AssignedAtToEC { get; set; }
        public string LeadSource { get; set; }
        public string LeadStatus { get; set; }
    }
}