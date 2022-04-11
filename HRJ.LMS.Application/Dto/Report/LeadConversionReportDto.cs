namespace HRJ.LMS.Application.Dto
{
    public class LeadConversionReportDto
    {
        public string Month { get; set; }
        public int LeadCount { get; set; }
        public decimal LeadValueInINR { get; set; }
        public decimal AverageLeadValue { get; set; }
        public string ExperienceCenterName { get; set; }
    }
}