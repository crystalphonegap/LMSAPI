using System.Collections.Generic;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using static HRJ.LMS.Application.Report.BusinessConversionReport;

namespace HRJ.LMS.Application.Interfaces
{
    public interface IExcelReportAccessor
    {
        Task<byte[]> WriteECManagerLeadToExcelFile(List<ECManagerLeadDataDto> ECManagerLeads);
        Task<byte[]> WriteKPOAgentLeadToExcelFile(List<KPOAgentLeadDataDto> KPOAgentLeads);
        Task<byte[]> WriteStateConversionLeadsToExcelFile(List<StateSourceConversionDto> StateSourceConversionLeads, AppLead.LeadMasterInfo.LeadMasterInfoEnvelope masterdata);

        Task<byte[]> WriteBusinessConversionLeadsToExcelFile(BusinessConversionReportEnvelope BusinessConversionEnvelope);
    }
}