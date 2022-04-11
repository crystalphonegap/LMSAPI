using System;

namespace HRJ.LMS.Domain
{
    public class AppUserState
    {
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        public State State { get; set; }
    }
}