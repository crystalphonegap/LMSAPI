using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto.User;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRJ.LMS.Application.User
{
    public class RefreshToken
    {
        public class RefreshTokenQuery : IRequest<UserDto>
        {
            public string Token { get; set; }
            public string UserName { get; set; }
            public string RefreshToken { get; set; }
        }

        public class Handler : IRequestHandler<RefreshTokenQuery, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
            }
            public async Task<UserDto> Handle(RefreshTokenQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserName);

                if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry < DateTime.Now)
                    throw new RestException(HttpStatusCode.NotAcceptable, new { message = "Invalid refresh token"} );
                
                user.RefreshToken = _jwtGenerator.CreateRefreshToken();
                user.RefreshTokenExpiry = DateTime.Now.AddDays(30);
                await _userManager.UpdateAsync(user);

                var userRoles = await _userManager.GetRolesAsync(user);
                
                return new UserDto
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Token = _jwtGenerator.CreateToken(user, userRoles),
                    RefreshToken = user.RefreshToken,
                    ChangePassword = user.ChangePassword
                };
            }
        }
    }
}