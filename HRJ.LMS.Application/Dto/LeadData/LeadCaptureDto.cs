using System;
using System.Collections.Generic;

namespace HRJ.LMS.Application.Dto
{
    public class LeadCaptureDto
    {

        public string LeadSourceId { get; set; }
        public DateTime LeadDateTime { get; set; }
        public string EnquiryType { get; set; }
        public string ContactPersonName { get; set; }
        public List<LeadContactDetailDto> LeadContactDetails { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string LeadSource { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}