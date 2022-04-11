using System.Linq;
using System.Security.Claims;
using HRJ.LMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HRJ.LMS.Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserId()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User?
                .Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            return currentUserId;
        }

        public string GetCurrentUserRole()
        {
            var currentUserRole = _httpContextAccessor.HttpContext.User?
                .Claims?.FirstOrDefault(i => i.Type == ClaimTypes.Role).Value;

            return currentUserRole;
        }

        public string GetCurrentUserName()
        {
            var currentUserName = _httpContextAccessor.HttpContext.User?
                .Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            
            return currentUserName;
        }
    }
}