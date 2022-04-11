using System;

namespace HRJ.LMS.Application.Dto
{
    public class UploadExcelLogDto
    {
        public string ExcelFileName { get; set; }
        public string UploadedRemarks { get; set; }
        public string UploadedByName { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}