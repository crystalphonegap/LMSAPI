using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRJ.LMS.Domain
{
    public class Lead
    {
        public Guid Id { get; set; }
        public DateTime LeadDateTime { get; set; }
        public string EnquiryType { get; set; }
        public string ContactPersonName { get; set; }
        public List<LeadContactDetail> LeadContactDetails { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public LeadCallingStatus LeadCallingStatus { get; set; }
        public int? LeadCallingStatusId { get; set; }
        public AppUser CalledBy { get; set; }
        /* public string CallerRemarks { get; set; } */
        public List<LeadCallerRemark> LeadCallerRemarks { get; set; }
        public List<LeadECManagerRemark> LeadECManagerRemarks { get; set; }
        public LeadClassification LeadClassification { get; set; }
        public int? LeadClassificationId { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public int? LeadStatusId { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal? QuantityInSquareFeet { get; set; }
        public LeadEnquiryType LeadEnquiryType { get; set; }
        public LeadSpaceType LeadSpaceType { get; set; }
        public ExperienceCenter AssignedToEC { get; set; }
        public int? AssignedToECId { get; set; }
        public DateTime? AssignedAtToEC { get; set; }
        public AppUser AssignedToSalesPerson { get; set; }
        public string SalesPersonRemarks { get; set; }
        public DateTime? LeadConversion { get; set; }
        public string DealerName { get; set; }
        public string DealerCode { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? LeadValueINR { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal? VolumeInSquareFeet { get; set; }
        public string FutureRequirement { get; set; }
        public string FutureRequirementTileSegment { get; set; }
        public string FutureRequirementVolume { get; set; }
        public string LeadSource { get; set; }
        public string LeadSourceId { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int IsLeadUpdated { get; set; }
        public List<LeadActivity> LeadActivities { get; set; }
        public List<LeadInvoiceFileDetail> LeadInvoiceFileDetails { get; set; }
    }
}