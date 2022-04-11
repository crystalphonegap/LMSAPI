using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using HRJ.LMS.Application.Dto.User;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRJ.LMS.Application.User
{
    public class Login
    {
        public class Query : IRequest<UserDto>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        /* public class QueryValidation : AbstractValidator<Query>
        {
            public QueryValidation()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        } */

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly AppDbContext _context;
            public Handler(UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator, AppDbContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
                _context = context;
            }
        public async Task<UserDto> Handle(Query request,
            CancellationToken cancellationToken)
        {
            //handler logic goes here
            var user = await _userManager.FindByEmailAsync(request.UserName);

            if (user == null) 
                user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { message = "Invalid Username or Password"});

            if (user.Status != 2)
                throw new RestException(HttpStatusCode.NotFound, new { message = "User not activated, kindly contact Prism Johnson Team" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (result.IsLockedOut)
                throw new RestException(HttpStatusCode.Forbidden, new { message = "Account Locked: Due to Too Many Failed Login Attempts" });

            var userRoles = await _userManager.GetRolesAsync(user);
            if (result.Succeeded && !user.ChangePassword)
            {
                user.RefreshToken = _jwtGenerator.CreateRefreshToken();
                user.RefreshTokenExpiry = DateTime.Now.AddDays(30);
                await _userManager.UpdateAsync(user);

                return new UserDto
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Token = _jwtGenerator.CreateToken(user, userRoles),
                    ChangePassword = user.ChangePassword,
                    RefreshToken = user.RefreshToken,
                };
            }
            else if (result.Succeeded && user.ChangePassword)
            {
                return new UserDto
                {
                    UserName = user.UserName,
                    ChangePassword = user.ChangePassword,
                    Token = _jwtGenerator.CreateToken(user, userRoles)
                };
            }

            throw new RestException(HttpStatusCode.NotFound, new { message = "Invalid Username or Password" });
        }
    }
}
}