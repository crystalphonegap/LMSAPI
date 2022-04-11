using System.Collections.Generic;

namespace HRJ.LMS.Application.Dto
{
    public class BusinessConversionDataDto
    {
        /* public string Month { get; set; }
        public int ConvertedLeads { get; set; }
        public decimal ConversionValue { get; set; } */
        public string ExperienceCenterShortName { get; set; }
        public string TeamName { get; set; }
        public List<LeadConversionDto> LeadConversions { get; set; }
    }
}