using System.Collections.Generic;
using HRJ.LMS.Domain;

namespace HRJ.LMS.Application.Interfaces
{
    public interface IJwtGenerator
    {
         string CreateRefreshToken();
        string CreateToken(AppUser user, IList<string> roles);
    }
}