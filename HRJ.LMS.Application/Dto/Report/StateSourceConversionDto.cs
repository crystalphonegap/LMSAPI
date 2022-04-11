
namespace HRJ.LMS.Application.Dto
{
    public class StateSourceConversionDto
    {
        public string StateName { get; set; }
        public string LeadSource { get; set; }
        public int LeadCount { get; set; }
        public int QualifiedLeads { get; set; }
        public int AssignedLeads { get; set; }
        public int ConvertedLeads { get; set; }        
        public decimal ConvertedLeadValue { get; set; }
        public decimal ContributionConversion { get; set; }
    }
}