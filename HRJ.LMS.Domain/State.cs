using System;

namespace HRJ.LMS.Domain
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string Region { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}