using System;

namespace HRJ.LMS.Application.Dto.LeadData
{
    public class LeadDetailSaveDto
    {
        public Guid Id { get; set; }
        public string EnquiryType { get; set; }
        public string ContactPersonName { get; set; }
        /* public List<LeadContactDetailDto> LeadContactDetails { get; set; } */
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int LeadCallingStatusId { get; set; }
        /* public AppUser CalledById { get; set; } */
        public string CallerRemarks { get; set; }
        public int LeadClassificationId { get; set; }
        public int LeadStatusId { get; set; }
        public decimal QuantityInSquareFeet { get; set; }
        public int LeadEnquiryTypeId { get; set; }
        public int LeadSpaceTypeId { get; set; }
        public int AssignedToECId { get; set; }
        public string ECRemarks { get; set; }
        /* public AppUser AssignedToSalesPerson { get; set; } */
        public string SalesPersonRemarks { get; set; }
        public DateTime? LeadConversion { get; set; }
        public string DealerName { get; set; }
        public string DealerCode { get; set; }
        public decimal LeadValueINR { get; set; }
        public decimal VolumeInSqureFeet { get; set; }
        public string FutureRequirement { get; set; }
        public string FutureRequirementTileSegment { get; set; }
        public string FutureRequirementVolume { get; set; }
        public string LeadSource { get; set; }
    }
}