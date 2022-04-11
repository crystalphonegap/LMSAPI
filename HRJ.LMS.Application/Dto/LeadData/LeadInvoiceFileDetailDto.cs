using System;

namespace HRJ.LMS.Application.Dto
{
    public class LeadInvoiceFileDetailDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedAt { get; set; }
        public string UploadedBy { get; set; }
        public bool IsActive { get; set; }
    }
}