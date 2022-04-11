using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRJ.LMS.Application.User
{
    public class ChangePassword
    {
        public class ChangePasswordCommand : IRequest<BaseDto>
        {
            public string UserName { get; set; }
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }

        public class Handler : IRequestHandler<ChangePasswordCommand, BaseDto>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            public Handler(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _context = context;
            }

            public async Task<BaseDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                    user = await _userManager.FindByEmailAsync(request.UserName);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { message = "User not found" });

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.CurrentPassword, false);

                if (!result.Succeeded)
                    throw new RestException(HttpStatusCode.Unauthorized, new { message = "Invalid Username or Password" });

                var passwordValidator = new PasswordValidator<AppUser>();
                var passwordResult = await passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    List<string> passwordErrors = new List<string>();
                    foreach (var error in passwordResult.Errors)
                    {
                        passwordErrors.Add(error.Description);   
                    }
                    throw new RestException(HttpStatusCode.InternalServerError, new { message = string.Join(", ", passwordErrors )});
                }
                
                var changePwdResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                if (!changePwdResult.Succeeded) 
                    throw new RestException(HttpStatusCode.InternalServerError, new { message = "Error Occurred while changing the password, Please contact to Prism Johnson Team" });
                

                user.ChangePassword = false;
                var success = await _userManager.UpdateAsync(user);


                if (success.Succeeded) 
                {
                    return new BaseDto 
                    {
                        Message = "Password Changed Successfully, Kindly Sign-in",
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                

                throw new Exception("Problem saving changes");
            }
        }
    }
}