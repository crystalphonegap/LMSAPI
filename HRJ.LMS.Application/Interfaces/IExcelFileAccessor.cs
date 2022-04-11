using System.Collections.Generic;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Domain;
using Microsoft.AspNetCore.Http;

namespace HRJ.LMS.Application.Interfaces
{
    public interface IExcelFileAccessor
    {
        Task<List<UploadLeadDto>> ReadExcelFile(IFormFile excelFile, List<UploadExcelTemplate> uploadExcelTemplate, List<StateCityMapping> stateCityMappings);

        Task<byte[]> WriteLeadToExcelFile(List<Lead> leads, List<UploadExcelTemplate> excelTemplate);
        
    }
}