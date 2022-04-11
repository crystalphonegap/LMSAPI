namespace HRJ.LMS.Application.Dto
{
    public class KPOAgentLeadDataDto
    {
        public string KPOAgentName { get; set; }
        public int LeadAssigned { get; set; }
        public int AttendedCalls { get; set; }
        public int PendingCalls { get; set; }
        public int LostToCompetitions { get; set; }
        public int Qualifieds { get; set; }
        public int NotReachables { get; set; }
        public int NotQualifieds { get; set; }
        public int Hot { get; set; }
        public int Warm { get; set; }
        public int Cold { get; set; }
    }
}