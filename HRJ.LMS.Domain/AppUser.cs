using System;
using Microsoft.AspNetCore.Identity;

namespace HRJ.LMS.Domain
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }    
        public int Status { get; set; }         //active or inactive
        public string UserType { get; set; }    //could be KPO Agent or EC Manager or Admin
        public bool ChangePassword { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}