using System.Collections.Generic;

namespace HRJ.LMS.Domain
{
    public class ExperienceCenter
    {
        public int Id { get; set; }
        public string ExperienceCenterName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public List<ECContactDetail> ECContactDetails { get; set; }
        public string ExperienceCenterShortName { get; set; }
        public string TeamName { get; set; }
    }
}