using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HRJ.LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        protected string GetUserId()
        {
            return this.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        }

        protected string GetUserRole()
        {
            return this.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Role).Value;
        }
    }
}