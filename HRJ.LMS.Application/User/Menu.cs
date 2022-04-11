using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto.User;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.User
{
    public class Menu
    {
        public class UserMenuQuery : IRequest<List<AppUserMenuDto>>
        {
            //public string CurrentUserId { get; set; }
        }

        public class Handler : IRequestHandler<UserMenuQuery, List<AppUserMenuDto>>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _userManager = userManager;
                _context = context;
            }
            public async Task<List<AppUserMenuDto>> Handle(UserMenuQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var userId = _userAccessor.GetCurrentUserId();

                var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));

                if (userRoles == null)
                    throw new RestException(HttpStatusCode.NotFound, new { message = "Roles not assigned to the user" });

                var userMenus = await _context.AppUserMenus
                                    .Where(x => userRoles.Contains(x.AppUserRole.Name))
                                    .OrderBy(x => x.RowOrder)
                                    .ToListAsync();

                if (userMenus == null)
                    throw new RestException(HttpStatusCode.NotFound, new { message = "Menus not assigned to User Role" });

                var userMenuReturn = _mapper.Map<List<AppUserMenu>, List<AppUserMenuDto>>(userMenus);

                return userMenuReturn;
            }
        }
    }
}