namespace HRJ.LMS.Application.Dto
{
    public class ECManagerLeadDataDto
    {
        public string ExperienceCenterName { get; set; }
        public int LeadAssigned { get; set; }
        public int AttendedCalls { get; set; }
        public int PendingCalls { get; set; }
        public int ClosedWithOtherBrands { get; set; }
        public int NoRequirements { get; set; }
        public int FollowUps { get; set; }
        public int ConvertedLeads { get; set; }
        public int NotReachables { get; set; }
        public int Hot { get; set; }
        public int Warm { get; set; }
        public int Cold { get; set; }
    }
}