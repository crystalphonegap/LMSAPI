using Microsoft.AspNetCore.Identity;

namespace HRJ.LMS.Domain
{
    public class AppUserRole : IdentityRole
    {
        public string Description { get; set; }
    }
}