using System.Threading.Tasks;
using HRJ.LMS.Application.AppLead;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HRJ.LMS.API.Controllers
{
    [ApiKey]
    public class LeadSourceController : BaseController
    {
        [HttpPost("postLeadData")]
        public async Task<BaseDto> PostJustDialLead(LeadJustDial.LeadJustDialCommand leadJustDial)
        {
            return await Mediator.Send(leadJustDial);
        }

        [HttpPost("postWebLead")]
        public async Task<BaseDto> PostWebLead(LeadWebPortal.LeadWebPortalCommand leadWebPortal)
        {
            return await Mediator.Send(leadWebPortal);
        }
    }
}