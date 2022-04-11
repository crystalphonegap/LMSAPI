using System;

namespace HRJ.LMS.Application.Dto
{
    public class UploadLeadDto
    {
        public int SerialNumber { get; set; }
        public DateTime LeadDateTime { get; set; }
        public string ContactPersonName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ContactType { get; set; }
        public string EnquireFor { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Subject { get; set; }
        public string CategoryName { get; set; }
        public string LeadSource { get; set; }
        public string StageOfConstruction { get; set; }
        public string Company { get; set; }
        public string MobileNumber2 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string EmailAddress2 { get; set; }
    }
}