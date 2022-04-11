using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class ExperienceCenterData
    {
        public class ExperienceCenterQuery : IRequest<List<ExperienceCenterDto>> { }

        public class Handler : IRequestHandler<ExperienceCenterQuery, List<ExperienceCenterDto>>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            public Handler(AppDbContext context, UserManager<AppUser> userManager, IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _context = context;
            }
            public async Task<List<ExperienceCenterDto>> Handle(ExperienceCenterQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here

                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { message = "Invalid user" });

                var experienceCentersQueryable = _context.ExperienceCenters.OrderBy(x => x.ExperienceCenterName);

                if ("ECManager".Equals(_userAccessor.GetCurrentUserRole()))
                {
                    experienceCentersQueryable = _context.AppUserExperienceCenters
                                        .Where(x => x.AppUser.Id == user.Id)
                                        .Include(x => x.ExperienceCenter)
                                        .Select(x => x.ExperienceCenter)
                                        .OrderBy(x => x.ExperienceCenterName);
                }

                var experienceCenters = await experienceCentersQueryable.ToListAsync();

                var experienceCenterDto = _mapper.Map<List<ExperienceCenter>, List<ExperienceCenterDto>>(experienceCenters);

                return experienceCenterDto;
            }
        }
    }
}