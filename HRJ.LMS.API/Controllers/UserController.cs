using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Dto.User;
using HRJ.LMS.Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HRJ.LMS.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("signIn")]
        public async Task<UserDto> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [Authorize]
        [HttpPut("changePassword/{userName}")]
        public async Task<BaseDto> ChangePassword(string userName, ChangePassword.ChangePasswordCommand command)
        {
            command.UserName = userName;
            return await Mediator.Send(command);
        }

        [Authorize]
        [HttpGet("appUserMenu")]
        public async Task<List<AppUserMenuDto>> GetAppUserMenu()
        {
            return await Mediator.Send(new Menu.UserMenuQuery());
        }

        [AllowAnonymous]
        [HttpPost("refreshToken")]
        public async Task<ActionResult<UserDto>> RefreshToken(RefreshToken.RefreshTokenQuery query)
        {
            var principal = GetPrincipalFromExpiredToken(query.Token);

            query.UserName = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return await Mediator.Send(query);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.
                Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return principal;
        }
    }
}