namespace HRJ.LMS.Domain
{
    public class LeadReminderOption
    {
        public int Id { get; set; }
        public string Reminder { get; set; }
        public int Duration { get; set; }
        public int DurationType { get; set; }
        public string DurationTypeInString { get; set; }
        public int RowOrder { get; set; }
    }
}