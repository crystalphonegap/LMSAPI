using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HRJ.LMS.Application.AppLead;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Extensions;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace HRJ.LMS.Infrastructure.Utilities
{
    public class ExcelFileAccessor : IExcelFileAccessor
    {
        private readonly IHostEnvironment _env;
        public ExcelFileAccessor(IHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<UploadLeadDto>> ReadExcelFile(IFormFile excelFile, List<UploadExcelTemplate> uploadExcelTemplate, List<StateCityMapping> stateCityMappings)
        {
            //check file extension if not xls or xlsx throw exception

            if (!(excelFile.FileName.EndsWith(".xlsx") || excelFile.FileName.EndsWith(".xls")))
            {
                throw new Exception("Invalid uploaded excel file");
            }

            var uploadLeadList = new List<UploadLeadDto>();

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    var colCount = worksheet.Dimension.Columns;
                    var rowCount = worksheet.Dimension.Rows;

                    var templateColCount = uploadExcelTemplate
                                            .GroupBy(x => new { x.LeadSource })
                                            .Select(x => x.Count());

                    if (!templateColCount.Contains(colCount))
                    {
                        throw new Exception("Uploaded excel file not in defined template");
                    }

                    uploadLeadList = MatchColumnsAndMapData(worksheet, uploadExcelTemplate, colCount, stateCityMappings);
                }
            }

            return uploadLeadList;
        }


        public async Task<byte[]> WriteLeadToExcelFile(List<Lead> leads, List<UploadExcelTemplate> excelTemplate)
        {
            using (var stream = new MemoryStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Lead Report");

                    foreach (var col in excelTemplate)
                    {
                        sheet.Cells[1, col.ColumnOrder].Value = col.ColumnName;
                    }

                    var row = 2;
                    await Task.Run(() =>
                    {
                        foreach (var lead in leads)
                        {
                            var col = 1;
                            var primaryContact = lead.LeadContactDetails.Where(x => x.ContactType == "Primary").FirstOrDefault();
                            var secondaryContact = lead.LeadContactDetails.Where(x => x.ContactType == "Secondary").FirstOrDefault();
                            sheet.Cells[row, col++].Value = new DateTime(lead.LeadDateTime.Year, lead.LeadDateTime.Month, lead.LeadDateTime.Day);  // 1
                            sheet.Cells[row, col++].Value = lead.LeadDateTime.ToString("HH:mm:ss"); //2
                            sheet.Cells[row, col++].Value = lead.LeadSource;    //3

                            //lead status
                            col = 4;
                            /*  sheet.Cells[row, col].Value = "Open";   */  //4
                            if (lead.LeadCallingStatusId == 2 || lead.LeadCallingStatusId == 3 || lead.LeadCallingStatusId == null || lead.LeadStatusId == 4 || lead.LeadStatusId == null)
                            {
                                //sheet.Cells[row, col++].Value = "Open";
                                sheet.Cells[row, col].Value = "Open";
                            }
                            else
                            {
                                //sheet.Cells[row, col++].Value = "Closed";   //4
                                sheet.Cells[row, col].Value = "Closed";
                            }

                            //if (LeadStatusCode.CallingStatus_NotReachable
                            //            .Equals(lead.LeadCallingStatus?.CallingStatus, StringComparison.OrdinalIgnoreCase)
                            //        || lead.LeadStatus == null
                            //        || LeadStatusCode.LeadStatus_Follow_up
                            //                .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase))
                            //{
                            //    sheet.Cells[row, col++].Value = "Open";     //4
                            //}

                            //if (LeadStatusCode.CallingStatus_NotQualified
                            //        .Equals(lead.LeadCallingStatus?.CallingStatus, StringComparison.OrdinalIgnoreCase)
                            //    || LeadStatusCode.CallingStatus_Lost_To_Competition
                            //        .Equals(lead.LeadCallingStatus?.CallingStatus, StringComparison.OrdinalIgnoreCase)
                            //    || LeadStatusCode.LeadStatus_Closed_with_HRJ
                            //        .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase)
                            //    || LeadStatusCode.LeadStatus_Closed_with_other_brand
                            //        .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase)
                            //    || LeadStatusCode.LeadStatus_No_Requirement
                            //        .Equals(lead.LeadStatus?.Status, StringComparison.OrdinalIgnoreCase))
                            //{
                            //    sheet.Cells[row, col++].Value = "Closed";   //4
                            //}


                            sheet.Cells[row, 5].Value = lead.ContactPersonName; //5
                            sheet.Cells[row, 7].Value = lead.Subject;   //7
                            sheet.Cells[row, 8].Value = lead.Description;

                            if (primaryContact != null)
                            {
                                sheet.Cells[row, 6].Value = primaryContact.EmailAddress;
                                sheet.Cells[row, 9].Value = primaryContact.MobileNumber;
                                sheet.Cells[row, 11].Value = primaryContact.PhoneNumber;
                            }

                            if (secondaryContact != null)
                            {
                                sheet.Cells[row, 10].Value = secondaryContact.MobileNumber;
                                sheet.Cells[row, 12].Value = secondaryContact.PhoneNumber;
                                sheet.Cells[row, 13].Value = secondaryContact.EmailAddress;
                            }

                            col = 14;
                            sheet.Cells[row, col++].Value = lead.EnquiryType;   //14
                            sheet.Cells[row, col++].Value = lead.Company;       //15
                            sheet.Cells[row, col++].Value = lead.Address;       //16
                            sheet.Cells[row, col++].Value = lead.City;          //17
                            sheet.Cells[row, col++].Value = lead.State;         //18
                            sheet.Cells[row, col++].Value = lead.AssignedToEC?.ExperienceCenterName;    //19
                            sheet.Cells[row, col++].Value = lead.LeadCallingStatus?.CallingStatus;      //20
                            sheet.Cells[row, col++].Value = lead.CalledBy?.FullName;                    //21

                            var callerRemarks = lead.LeadCallerRemarks
                                                    .OrderByDescending(x => x.CallerRemarkAt)
                                                    .FirstOrDefault();
                            sheet.Cells[row, col++].Value = callerRemarks?.CallerRemark;       //22
                            sheet.Cells[row, col++].Value = lead.LeadClassification?.Classification;       //23
                            sheet.Cells[row, col++].Value = lead.QuantityInSquareFeet;         //24
                            sheet.Cells[row, col++].Value = lead.LeadEnquiryType?.EnquiryType; //25
                            sheet.Cells[row, col++].Value = lead.LeadSpaceType?.SpaceType;     //26

                            //sheet.Cells[row, 26].Value = lead;    //assigned to sales officer
                            //sheet.Cells[row, 27].Value = lead;    //sales officer remarks
                            col = 29;
                            var ecRemarks = lead.LeadECManagerRemarks
                                                    .OrderByDescending(x => x.ECManagerRemarkAt)
                                                    .FirstOrDefault();
                            sheet.Cells[row, col++].Value = ecRemarks?.ECManagerRemark;      //29
                            sheet.Cells[row, col++].Value = lead.LeadStatus?.Status;         //30
                            sheet.Cells[row, col++].Value = lead.LeadConversion;             //31
                            sheet.Cells[row, col++].Value = lead.DealerName;           //32
                            sheet.Cells[row, col++].Value = lead.DealerCode;           //33
                            sheet.Cells[row, col++].Value = lead.LeadValueINR;         //34
                            sheet.Cells[row, col++].Value = lead.VolumeInSquareFeet;   //35
                            sheet.Cells[row, col++].Value = lead.FutureRequirement;    //36
                            sheet.Cells[row, col++].Value = lead.FutureRequirementTileSegment; //37
                            sheet.Cells[row, col++].Value = lead.FutureRequirementVolume;      //38
                            sheet.Cells[row, col++].Value = lead.LastUpdatedAt;      //39
                            row++;
                        }
                        sheet.Cells[1, 1, row - 1, 1].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    });


                    return package.GetAsByteArray();
                }
            }
        }

        private List<UploadLeadDto> MatchColumnsAndMapData(ExcelWorksheet worksheet, List<UploadExcelTemplate> excelTemplate, int colCount, List<StateCityMapping> stateCityMappings)
        {
            var uploadLeadList = new List<UploadLeadDto>();
            var leadSources = excelTemplate.Select(x => x.LeadSource).Distinct();

            foreach (var source in leadSources)
            {
                try
                {



                    var columns = excelTemplate
                            .Where(x => x.LeadSource == source)
                            .OrderBy(x => x.ColumnOrder);


                    var matched = false;
                    foreach (var col in columns)
                    {
                        if (col.ColumnOrder <= colCount && col.ColumnName.Equals(worksheet.Cells[1, col.ColumnOrder].Value.ToString()))
                        {
                            matched = true;
                        }
                        else
                        {
                            matched = false;
                        }
                    }

                    if (matched)
                    {
                        try
                        {



                            if (source == "WebsiteLeads")
                            {
                                return MapWebsiteLeadData(worksheet);
                            }
                            else if (source == "Just Dial")
                            {
                                return MapJustDialLeadData(worksheet, source, stateCityMappings);
                            }
                            else if (source == "Cement")
                            {
                                return MapCementLeadData(worksheet, source);
                            }
                            else if (source == "Generic")
                            {
                                return MapGenericLeadData(worksheet);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }


                }
                catch (Exception ex)
                {
                   
                }

            }
            //throw exception
            return uploadLeadList;

        }

        private List<UploadLeadDto> MapWebsiteLeadData(ExcelWorksheet worksheet)
        {
            var uploadLeadList = new List<UploadLeadDto>();

            var colCount = worksheet.Dimension.Columns;
            var rowCount = worksheet.Dimension.Rows;
            try
            {


                for (int row = 2; row <= rowCount; row++)
                {
                    uploadLeadList.Add(new UploadLeadDto
                    {
                        SerialNumber = worksheet.Cells[row, 1].GetValue<int>(),
                        LeadDateTime = worksheet.Cells[row, 2].GetValue<DateTime>(),
                        ContactPersonName = worksheet.Cells[row, 3].GetValue<string>(),
                        EmailAddress = worksheet.Cells[row, 4].GetValue<string>(),
                        MobileNumber = worksheet.Cells[row, 5].GetValue<string>(),
                        ContactType = "Primary",
                        Subject = worksheet.Cells[row, 6].GetValue<string>(),
                        State = worksheet.Cells[row, 7].GetValue<string>(),
                        City = worksheet.Cells[row, 8].GetValue<string>(),
                        Description = worksheet.Cells[row, 9].GetValue<string>(),
                        EnquireFor = worksheet.Cells[row, 10].GetValue<string>(),
                        LeadSource = GetLeadSource(worksheet.Cells[row, 10].GetValue<string>()),
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return uploadLeadList;
        }

        private List<UploadLeadDto> MapGenericLeadData(ExcelWorksheet worksheet)
        {
            var uploadLeadList = new List<UploadLeadDto>();

            var colCount = worksheet.Dimension.Columns;
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {


                    uploadLeadList.Add(new UploadLeadDto
                    {
                        LeadDateTime = GetConvertedDateTime(worksheet.Cells[row, 1].GetValue<DateTime>().ToString("dd-MM-yyyy"),
                                 worksheet.Cells[row, 2].GetValue<DateTime>().ToString("HH:mm:ss")),
                        EnquireFor = worksheet.Cells[row, 3].GetValue<string>(),
                        LeadSource = GetLeadSource(worksheet.Cells[row, 3].GetValue<string>()),
                        ContactPersonName = worksheet.Cells[row, 4].GetValue<string>(),
                        EmailAddress = worksheet.Cells[row, 5].GetValue<string>(),
                        Subject = worksheet.Cells[row, 6].GetValue<string>(),
                        Description = worksheet.Cells[row, 7].GetValue<string>(),
                        MobileNumber = worksheet.Cells[row, 8].GetValue<string>(),
                        MobileNumber2 = worksheet.Cells[row, 9].GetValue<string>(),
                        PhoneNumber = worksheet.Cells[row, 10].GetValue<string>(),
                        PhoneNumber2 = worksheet.Cells[row, 11].GetValue<string>(),
                        EmailAddress2 = worksheet.Cells[row, 12].GetValue<string>(),
                        Company = worksheet.Cells[row, 13].GetValue<string>(),
                        Address = worksheet.Cells[row, 14].GetValue<string>(),
                        City = worksheet.Cells[row, 15].GetValue<string>(),
                        State = worksheet.Cells[row, 16].GetValue<string>(),
                        ContactType = "Primary",
                    });
                }
                catch (Exception ex)
                {

                }

            }

            return uploadLeadList;
        }

        private string GetLeadSource(string leadSource)
        {
            switch (leadSource)
            {
                case "fb":
                    return "Social Media";
                case "ig":
                    return "Social Media";
                default:
                    return leadSource;
            }
        }

        private List<UploadLeadDto> MapJustDialLeadData(ExcelWorksheet worksheet, string source, List<StateCityMapping> stateCityMappings)
        {
            var uploadLeadList = new List<UploadLeadDto>();

            var colCount = worksheet.Dimension.Columns;
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {


                    var uploadLead = new UploadLeadDto
                    {
                        SerialNumber = worksheet.Cells[row, 1].GetValue<int>(),
                        LeadDateTime = GetConvertedDateTime(worksheet.Cells[row, 2].GetValue<DateTime>().ToString("dd-MM-yyyy"),
                                worksheet.Cells[row, 3].GetValue<DateTime>().ToString("HH:mm:ss")),
                        ContactPersonName = worksheet.Cells[row, 5].GetValue<string>(),
                        PhoneNumber = worksheet.Cells[row, 6].GetValue<string>(),
                        MobileNumber = worksheet.Cells[row, 7].GetValue<string>(),
                        EmailAddress = worksheet.Cells[row, 8].GetValue<string>(),
                        ContactType = "Primary",
                        CategoryName = worksheet.Cells[row, 9].GetValue<string>(),
                        Subject = worksheet.Cells[row, 9].GetValue<string>(),
                        Address = worksheet.Cells[row, 10].GetValue<string>(),
                        City = worksheet.Cells[row, 11].GetValue<string>(),
                        LeadSource = source
                    };

                    var stateCityMapping = stateCityMappings
                                .Where(x => x.City.ToLower().Equals(uploadLead.City.ToLower()))
                                .FirstOrDefault();

                    if (stateCityMapping != null)
                    {
                        uploadLead.State = stateCityMapping.StateName;
                    }

                    uploadLeadList.Add(uploadLead);
                }
                catch (Exception ex)
                {

                }

            }

            return uploadLeadList;
        }

        private DateTime GetConvertedDateTime(string date, string time)
        {
            if (date.Length != 10 && date.Length != 19)
                return DateTime.Now;

            var splittedDate = date.Split("-");

            if (!string.IsNullOrEmpty(time))
            {
                var splittedTime = time.Split(" ");

                if (splittedTime.Count() == 2)
                {
                    time = splittedTime[1];
                }
            }

            var newDateTime = string.Empty;

            if (_env.IsProduction())    //Production Format
            {
                newDateTime = string.Format("{0}/{1}/{2} {3}",
                    splittedDate[0], splittedDate[1], splittedDate[2].Substring(0, 4), time);
            }
            else if (_env.IsStaging()) //QA Format
            {
                newDateTime = string.Format("{1}/{0}/{2} {3}",
                    splittedDate[0], splittedDate[1], splittedDate[2].Substring(0, 4), time);
            }
            else if (_env.IsDevelopment()) //Dev Format
            {
                newDateTime = string.Format("{0}/{1}/{2} {3}",
                    splittedDate[0], splittedDate[1], splittedDate[2].Substring(0, 4), time);
            }



            return Convert.ToDateTime(newDateTime);
        }

        private List<UploadLeadDto> MapCementLeadData(ExcelWorksheet worksheet, string source)
        {
            var uploadLeadList = new List<UploadLeadDto>();

            var colCount = worksheet.Dimension.Columns;
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    uploadLeadList.Add(new UploadLeadDto
                    {
                        SerialNumber = worksheet.Cells[row, 1].GetValue<int>(),
                        State = worksheet.Cells[row, 2].GetValue<string>(),
                        LeadDateTime = worksheet.Cells[row, 9].GetValue<DateTime>(),
                        ContactPersonName = worksheet.Cells[row, 10].GetValue<string>(),
                        Address = worksheet.Cells[row, 11].GetValue<string>(),
                        City = worksheet.Cells[row, 12].GetValue<string>(),
                        MobileNumber = worksheet.Cells[row, 13].GetValue<string>(),
                        ContactType = "Primary",
                        StageOfConstruction = worksheet.Cells[row, 14].GetValue<string>(),
                        Subject = worksheet.Cells[row, 14].GetValue<string>(),
                        Description = string.Format("Region:{0}-{1}-{2}\nPrism Contact Person:{3},{4}" +
                                "\nStage Of Construction:{5}, Lead Status:{6}\nServices Offered:",
                                    worksheet.Cells[row, 3].GetValue<string>(), //Region
                                    worksheet.Cells[row, 4].GetValue<string>(), //branch
                                    worksheet.Cells[row, 5].GetValue<string>(), //territory
                                    worksheet.Cells[row, 6].GetValue<string>(), //contact person name
                                    worksheet.Cells[row, 7].GetValue<string>(), //contact person mobile
                                    worksheet.Cells[row, 14].GetValue<string>(), //stage of construction
                                    worksheet.Cells[row, 17].GetValue<string>(), //lead status
                                    worksheet.Cells[row, 21].GetValue<string>() //services Offered
                                    ),

                        //region branch territory name of office, con
                        LeadSource = source
                    });
                }
                catch (Exception ex)
                {

                }
            }

            return uploadLeadList;
        }
    }
}