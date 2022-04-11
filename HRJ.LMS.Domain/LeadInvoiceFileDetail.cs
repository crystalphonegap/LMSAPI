using System;

namespace HRJ.LMS.Domain
{
    public class LeadInvoiceFileDetail
    {
        public Guid Id { get; set; }
        public Lead Lead { get; set; }
        public string FileName { get; set; }
        public string SystemFileName { get; set; }
        public bool IsActive { get; set; }
        public DateTime UploadedAt { get; set; }
        public AppUser UploadedBy { get; set; }
        public string UploadedByName { get; set; }
        public AppUser RemovedBy { get; set; }
        public DateTime RemovedAt { get; set; }
    }
}