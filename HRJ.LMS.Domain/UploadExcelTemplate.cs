namespace HRJ.LMS.Domain
{
    public class UploadExcelTemplate
    {
        public int Id { get; set; }
        public string LeadSource { get; set; }
        public string ColumnName { get; set; }
        public int ColumnOrder { get; set; }

    }
}