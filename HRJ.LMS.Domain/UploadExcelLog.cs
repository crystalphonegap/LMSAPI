using System;

namespace HRJ.LMS.Domain
{
    public class UploadExcelLog
    {
        public Guid Id { get; set; }
        public string ExcelFileName { get; set; }
        public string UploadedRemarks { get; set; }
        public AppUser UploadedBy { get; set; }
        public string UploadedById { get; set; }
        public string UploadedByName { get; set; }
        public DateTime UploadedAt { get; set; }

    }
}