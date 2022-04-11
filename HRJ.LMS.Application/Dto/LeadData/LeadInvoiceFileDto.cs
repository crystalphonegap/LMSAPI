
using System.IO;

namespace HRJ.LMS.Application.Dto
{
    public class LeadInvoiceFileDto
    {
        public MemoryStream FileContents { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}