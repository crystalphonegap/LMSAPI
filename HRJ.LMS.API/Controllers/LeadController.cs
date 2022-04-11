using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRJ.LMS.Application.AppLead;
using HRJ.LMS.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRJ.LMS.API.Controllers
{
    public class LeadController : BaseController
    {
        [Authorize]
        [HttpGet("getLeadList")]
        public async Task<LeadList.LeadListEnvelope> GetLeads([FromQuery]LeadList.LeadListQuery leadListQuery)
        {
            return await Mediator.Send(leadListQuery);
        }

        [Authorize]
        [HttpGet("getUpcomingLeadList")]
        public async Task<UpcomingLead.UpcomingLeadListEnvelope> GetUpcomingLeads([FromQuery]UpcomingLead.UpcomingLeadQuery upcomingLeadQuery)
        {
            return await Mediator.Send(upcomingLeadQuery);
        }

//http Verbs -- HttpGet, HttpPost, HttpPut
        [Authorize]
        [HttpGet("getLeadDetail/{leadId}")]
        public async Task<LeadDetailDto> GetLeadDetail(Guid leadId)
        {
            return await Mediator.Send(new LeadDetail.LeadDetailQuery() { LeadId = leadId });
        }

        [Authorize]
        [HttpPost("getExistingLeads")]
        public async Task<List<ExistingLeadDto>> GetExistingLeads(List<Guid> existingLeadIds)
        {
            return await Mediator.Send(new ExistingLead.ExistingLeadQuery() { ExistingLeadIds = existingLeadIds });
        }

       /*  [Authorize]
        [HttpGet("getExperienceCenter/{stateId}")]
        public async Task<LeadDetailDto> getExperienceCenter(int stateId)
        {
            return await Mediator.Send(new LeadDetail.LeadDetailQuery() { LeadId = leadId });
        } */

        [Authorize]
        [HttpGet("getLeadMasterData")]
        public async Task<LeadMasterInfo.LeadMasterInfoEnvelope> GetLeadMasterData()
        {
            return await Mediator.Send(new LeadMasterInfo.LeadMasterInfoQuery());
        }

        [Authorize]
        [HttpPut("saveLeadDetail/{leadId}")]
        public async Task<BaseDto> SaveLeadDetails(Guid leadId, SaveLead.SaveLeadCommand saveLeadCommand)
        {
            saveLeadCommand.Id = leadId;
            return await Mediator.Send(saveLeadCommand);
        }

        [Authorize]
        [HttpPost("uploadInvoiceFile/{leadId}")]
        public async Task<BaseDto> UploadInvoiceFile(Guid leadId, [FromForm]UploadInvoice.UploadInvoiceCommand uploadInvoiceCommand)
        {
            uploadInvoiceCommand.LeadId = leadId;
            return await Mediator.Send(uploadInvoiceCommand);
        }

        [Authorize]
        [HttpPost("UploadLead")]
        public async Task<BaseDto> UploadLead([FromForm]UploadLead.UploadLeadCommand uploadLeadCommand)
        {
            return await Mediator.Send(uploadLeadCommand);
        }
        

        [Authorize]
        [HttpGet("getUploadExcelLogs")]
        public async Task<UploadLeadLog.UploadLogEnvelope> GetUploadExcelLogs([FromQuery]UploadLeadLog.UploadLeadLogQuery uploadLeadLog)
        {
            return await Mediator.Send(uploadLeadLog);
        }
        

        [Authorize]
        [HttpGet("downloadInvoiceFile/{leadInvoiceFileId}")]
        public async Task<FileResult> DownloadInvoiceFile(Guid leadInvoiceFileId)
        {
            var invoiceFileContent = await Mediator.Send(new InvoiceFile.InvoiceFileQuery { LeadInvoiceFileId = leadInvoiceFileId});

            return File(invoiceFileContent.FileContents, invoiceFileContent.ContentType, invoiceFileContent.FileName);
        }
    }
}