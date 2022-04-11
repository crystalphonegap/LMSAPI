using System;

namespace HRJ.LMS.Domain
{
    public class AppUserExperienceCenter
    {
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        public ExperienceCenter ExperienceCenter { get; set; }
    }
}