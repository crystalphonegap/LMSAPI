using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Application.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HRJ.LMS.Application.Report.BusinessConversionReport;

namespace HRJ.LMS.API.Controllers
{
    public class LeadReportController : BaseController
    {
        private readonly IExcelFileAccessor _excelFileAccessor;
        private readonly IExcelReportAccessor _excelReportAccessor;
        public LeadReportController(IExcelFileAccessor excelFileAccessor, IExcelReportAccessor excelReportAccessor)
        {
            _excelReportAccessor = excelReportAccessor;
            _excelFileAccessor = excelFileAccessor;
        }

        [Authorize]
        [HttpGet("downloadLeadReport")]
        public async Task<FileResult> DownloadLeadReport([FromQuery] LeadReport.LeadReportQuery reportQuery)
        {
            var leadReportEnvelope = await Mediator.Send(reportQuery);

            var file = File(leadReportEnvelope.LeadReportInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", leadReportEnvelope.ReportFileName);

            return file;
        }

        [Authorize]
        [HttpGet("leadConversionData")]
        public async Task<List<LeadConversionReportDto>> LeadConversion([FromQuery] LeadConversionReport.LeadConversionReportQuery conversionReportQuery)
        {
            return await Mediator.Send(conversionReportQuery);
        }

        [Authorize]
        [HttpGet("businessConversionData")]
        public async Task<BusinessConversionReportEnvelope> BusinessConversion([FromQuery] BusinessConversionReport.BusinessConversionQuery conversionReportQuery)
        {
            return await Mediator.Send(conversionReportQuery);
        }

        [Authorize]
        [HttpGet("businessConversionDownload")]
        public async Task<FileResult> BusinessConversionDownload([FromQuery] BusinessConversionReport.BusinessConversionQuery conversionReportQuery)
        {
            var businessConversionEnvelope = await Mediator.Send(conversionReportQuery);

            var ReportFileName = string.Format("{0}_{1}.xlsx", "BusinessConversionLeadReport", string.Format(DateTime.Now.ToString(), "dd-MM-YYYY_HH:mm:ss"));

            var BusinessConversionLeadsInBytes = await _excelReportAccessor.WriteBusinessConversionLeadsToExcelFile(businessConversionEnvelope);

            var file = File(BusinessConversionLeadsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ReportFileName);

            return file;
        }

        [Authorize]
        [HttpGet("getFiscalYears")]
        public async Task<List<FiscalYearDto>> GetFiscalYears()
        {
            return await Mediator.Send(new FiscalYearData.FiscalYearDataQuery());
        }

        [Authorize]
        [HttpGet("getExperienceCenters")]
        public async Task<List<ExperienceCenterDto>> GetExperienceCenters()
        {
            return await Mediator.Send(new ExperienceCenterData.ExperienceCenterQuery());
        }

        [Authorize]
        [HttpGet("ecManagerLeadReport")]
        public async Task<List<ECManagerLeadDataDto>> ECManagerLeadReport([FromQuery] ECManagerLeadReport.ECManagerLeadReportQuery leadReportQuery)
        {
            return await Mediator.Send(leadReportQuery);
        }

        [Authorize]
        [HttpGet("ecManagerLeadDownload")]
        public async Task<FileResult> ECManagerLeadDownload([FromQuery] ECManagerLeadReport.ECManagerLeadReportQuery leadReportQuery)
        {
            var ecManagerLeadReport = await Mediator.Send(leadReportQuery);

            var ReportFileName = string.Format("{0}_{1}.xlsx", "ECManagerLeadReport", string.Format(DateTime.Now.ToString(), "dd-MM-YYYY_HH:mm:ss"));

            var ECManagerLeadsInBytes = await _excelReportAccessor.WriteECManagerLeadToExcelFile(ecManagerLeadReport);

            var file = File(ECManagerLeadsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ReportFileName);

            return file;
        }

        [Authorize]
        [HttpGet("kpoAgentLeadReport")]
        public async Task<List<KPOAgentLeadDataDto>> KPOAgentLeadReport([FromQuery] KPOAgentLeadReport.KPOAgentLeadReportQuery leadReportQuery)
        {
            return await Mediator.Send(leadReportQuery);
        }

        [Authorize]
        [HttpGet("kpoAgentLeadDownload")]
        public async Task<FileResult> KPOAgentLeadDownload([FromQuery] KPOAgentLeadReport.KPOAgentLeadReportQuery leadReportQuery)
        {
            var kpoAgentLeadReport = await Mediator.Send(leadReportQuery);

            var ReportFileName = string.Format("{0}_{1}.xlsx", "KPOAgentLeadReport", string.Format(DateTime.Now.ToString(), "dd-MM-YYYY_HH:mm:ss"));

            var KPOAgentLeadsInBytes = await _excelReportAccessor.WriteKPOAgentLeadToExcelFile(kpoAgentLeadReport);

            var file = File(KPOAgentLeadsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ReportFileName);

            return file;
        }

        [Authorize]
        [HttpGet("stateSourceConversionReport")]
        public async Task<List<StateSourceConversionDto>> StateSourceConversionReport([FromQuery] StateSourceConversionReport.StateSourceConversionReportQuery leadReportQuery)
        {
            return await Mediator.Send(leadReportQuery);
        }

        [Authorize]
        [HttpGet("stateSourceConversionDownload")]
        public async Task<FileResult> StateSourceConversionDownload([FromQuery] StateSourceConversionReport.StateSourceConversionReportQuery leadReportQuery)
        {
            var stateSourceConversionReport = await Mediator.Send(leadReportQuery);

            var masterData = await Mediator.Send(new Application.AppLead.LeadMasterInfo.LeadMasterInfoQuery());

            var ReportFileName = string.Format("{0}_{1}.xlsx", "StateSourceConversionReport", string.Format(DateTime.Now.ToString(), "dd-MM-YYYY_HH:mm:ss"));

            var StateSourceConversionInBytes = await _excelReportAccessor.WriteStateConversionLeadsToExcelFile(stateSourceConversionReport, masterData);

            var file = File(StateSourceConversionInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ReportFileName);

            return file;
        }

        [Authorize]
        [HttpGet("getLeadAttended")]
        public async Task<List<LeadAttendedDto>> GetLeadAttended([FromQuery] LeadAttendedReport.LeadAttendedReportQuery leadReportQuery)
        {
            return await Mediator.Send(leadReportQuery);
        }
    }
}