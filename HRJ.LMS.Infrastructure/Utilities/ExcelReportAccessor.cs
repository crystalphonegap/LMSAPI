using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HRJ.LMS.Application.AppLead;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using static HRJ.LMS.Application.Report.BusinessConversionReport;

namespace HRJ.LMS.Infrastructure.Utilities
{
    public class ExcelReportAccessor : IExcelReportAccessor
    {
        public async Task<byte[]> WriteECManagerLeadToExcelFile(List<ECManagerLeadDataDto> ECManagerLeads)
        {
            using (var stream = new MemoryStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("EC_Manager_Lead_Report");
                    var row = 1;
                    var col = 1;
                    //heading
                    sheet.Cells[row, col++].Value = "Sr. No.";
                    sheet.Cells[row, col++].Value = "Experience Center";
                    sheet.Cells[row, col++].Value = "Lead Assigned";
                    sheet.Cells[row, col++].Value = "Calls Done";
                    sheet.Cells[row, col++].Value = "Calls Pending";
                    sheet.Cells[row, col++].Value = "Closed with other Brands";
                    sheet.Cells[row, col++].Value = "No Requirement";
                    sheet.Cells[row, col++].Value = "Follow Up";
                    sheet.Cells[row, col++].Value = "Converted Leads";
                    sheet.Cells[row, col++].Value = "Not Reachable";
                    sheet.Cells[row, col++].Value = "Hot";
                    sheet.Cells[row, col++].Value = "Warm";
                    sheet.Cells[row, col++].Value = "Cold";

                    sheet.Cells["A1:M1"].Style.Font.Bold = true;
                    sheet.Cells["A1:M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["A1:M1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#C6E0B4"));
                    //data
                    row++;

                    await Task.Run(() =>
                    {
                        foreach (var data in ECManagerLeads)
                        {
                            col = 1;
                            sheet.Cells[row, col++].Value = row - 1;
                            sheet.Cells[row, col++].Value = data.ExperienceCenterName;
                            sheet.Cells[row, col++].Value = data.LeadAssigned;
                            sheet.Cells[row, col++].Value = data.AttendedCalls;
                            sheet.Cells[row, col++].Value = data.PendingCalls;
                            sheet.Cells[row, col++].Value = data.ClosedWithOtherBrands;
                            sheet.Cells[row, col++].Value = data.NoRequirements;
                            sheet.Cells[row, col++].Value = data.FollowUps;
                            sheet.Cells[row, col++].Value = data.ConvertedLeads;
                            sheet.Cells[row, col++].Value = data.NotReachables;
                            sheet.Cells[row, col++].Value = data.Hot;
                            sheet.Cells[row, col++].Value = data.Warm;
                            sheet.Cells[row, col++].Value = data.Cold;
                            row++;
                        }

                        sheet.Cells[string.Format("A{0}:M{1}", row - 1, row - 1)].Style.Font.Bold = true;
                        sheet.Cells[string.Format("A{0}:M{1}", row - 1, row - 1)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[string.Format("A{0}:M{1}", row - 1, row - 1)].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                        sheet.Cells[string.Format("A{0}:M{1}", 1, row - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                        sheet.Cells[string.Format("A{0}:M{1}", 1, row - 1)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[string.Format("A{0}:M{1}", 1, row - 1)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[string.Format("A{0}:M{1}", 1, row - 1)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[string.Format("A{0}:M{1}", 1, row - 1)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    });

                    return package.GetAsByteArray();
                }
            }

        }

        public async Task<byte[]> WriteKPOAgentLeadToExcelFile(List<KPOAgentLeadDataDto> KPOAgentLeads)
        {
            using (var stream = new MemoryStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("KPO_Agent_Lead_Report");
                    var row = 1;
                    var col = 1;
                    //heading
                    sheet.Cells[row, col++].Value = "Sr. No.";
                    sheet.Cells[row, col++].Value = "KPO Agent";
                    sheet.Cells[row, col++].Value = "Lead Assigned";
                    sheet.Cells[row, col++].Value = "Calls Done";
                    sheet.Cells[row, col++].Value = "Calls Pending";
                    sheet.Cells[row, col++].Value = "Lost to Competition";
                    sheet.Cells[row, col++].Value = "Not Qualified";
                    sheet.Cells[row, col++].Value = "Not Reachable";
                    sheet.Cells[row, col++].Value = "Qualified";
                    sheet.Cells[row, col++].Value = "Hot";
                    sheet.Cells[row, col++].Value = "Warm";
                    sheet.Cells[row, col++].Value = "Cold";

                    sheet.Cells["A1:L1"].Style.Font.Bold = true;
                    sheet.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#C6E0B4"));
                    //data
                    row++;

                    await Task.Run(() =>
                    {
                        foreach (var data in KPOAgentLeads)
                        {
                            col = 1;
                            sheet.Cells[row, col++].Value = row - 1;
                            sheet.Cells[row, col++].Value = data.KPOAgentName;
                            sheet.Cells[row, col++].Value = data.LeadAssigned;
                            sheet.Cells[row, col++].Value = data.AttendedCalls;
                            sheet.Cells[row, col++].Value = data.PendingCalls;
                            sheet.Cells[row, col++].Value = data.LostToCompetitions;
                            sheet.Cells[row, col++].Value = data.NotQualifieds;
                            sheet.Cells[row, col++].Value = data.NotReachables;
                            sheet.Cells[row, col++].Value = data.Qualifieds;
                            sheet.Cells[row, col++].Value = data.Hot;
                            sheet.Cells[row, col++].Value = data.Warm;
                            sheet.Cells[row, col++].Value = data.Cold;
                            row++;
                        }

                        sheet.Cells[string.Format("A{0}:L{1}", row - 1, row - 1)].Style.Font.Bold = true;
                        sheet.Cells[string.Format("A{0}:L{1}", row - 1, row - 1)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[string.Format("A{0}:L{1}", row - 1, row - 1)].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                        sheet.Cells[string.Format("A{0}:L{1}", 1, row - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                        sheet.Cells[string.Format("A{0}:L{1}", 1, row - 1)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[string.Format("A{0}:L{1}", 1, row - 1)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[string.Format("A{0}:L{1}", 1, row - 1)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[string.Format("A{0}:L{1}", 1, row - 1)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    });

                    return package.GetAsByteArray();
                }
            }

        }


        public async Task<byte[]> WriteStateConversionLeadsToExcelFile(List<StateSourceConversionDto> StateSourceConversionLeads, LeadMasterInfo.LeadMasterInfoEnvelope masterdata)
        {

            using (var stream = new MemoryStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Conversion Extensive Report");
                    var row = 1;
                    var col = 1;
                    //heading
                    sheet.Cells[row, col, row+1, col].Merge = true;
                    sheet.Cells[row, col, row+1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheet.Cells[row, col, row+1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells[row, col, row+1, col].Value = "States";
                    col++;

                    masterdata.LeadSources.Add
                    (
                        new LeadSourceDto
                        {
                            Source = "Total"
                        }
                    );
                    foreach(var source in masterdata.LeadSources)
                    {
                        sheet.Cells[row, col, row, col+5].Merge = true;
                        sheet.Cells[row, col, row, col+5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        sheet.Cells[row, col, row, col+5].Value = source.Source;
                        sheet.Cells[row+1, col++].Value = "Generated Leads";
                        sheet.Cells[row+1, col++].Value = "Qualified Leads";
                        sheet.Cells[row+1, col++].Value = "Converted Count";
                        sheet.Cells[row+1, col++].Value = "Conversion %";
                        sheet.Cells[row+1, col++].Value = "Conversion Value";
                        sheet.Cells[row+1, col++].Value = "Conversion Contribution";
                    }

                    row=3;

                    foreach(var states in masterdata.States)
                    {
                        sheet.Cells[row++, 1].Value = states.StateName;
                    }
                    
                    sheet.Cells[1,1,2, col-1].Style.Font.Bold = true;
                    sheet.Cells[1,1,2, col-1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[1,1,2, col-1].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    //data
                    row=3;
                    await Task.Run(() =>
                    {
                        foreach (var state in masterdata.States)
                        {                       
                            var stateWiseData = StateSourceConversionLeads.Where(x => x.StateName == state.StateName);
                            col = 2;
                            if (stateWiseData != null)
                            {
                                foreach (var source in masterdata.LeadSources)
                                {
                                    var data = stateWiseData.Where(x => x.LeadSource == source.Source).FirstOrDefault();
                                    if (data == null)
                                    {
                                        data = new StateSourceConversionDto();
                                    }
                                    //sheet.Cells[row, col++].Value = state.StateName;
                                    sheet.Cells[row, col++].Value = data.LeadCount;
                                    sheet.Cells[row, col++].Value = data.QualifiedLeads;
                                    sheet.Cells[row, col++].Value = data.ConvertedLeads;
                                    sheet.Cells[row, col].Style.Numberformat.Format = "0%";
                                    if (data.LeadCount > 0)
                                    {
                                        var value = Convert.ToDecimal(data.ConvertedLeads)/data.LeadCount;
                                        sheet.Cells[row, col++].Value = value;
                                    }
                                    else
                                    {
                                        sheet.Cells[row, col++].Value = 0;
                                    }
                                    sheet.Cells[row, col++].Value = data.ConvertedLeadValue;
                                    sheet.Cells[row, col].Style.Numberformat.Format = "0%";
                                    sheet.Cells[row, col++].Value = data.ContributionConversion;
                                }
                            } 
                            else 
                            {
                                sheet.Cells[row, col++].Value = state.StateName;
                                col=+5;
                            }

                            row++;
                        }

                        //sheet.Cells[1, 1, row-1, col].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                        sheet.Cells[row, 1].Value = "Grand Total";
                        var colIndex = 5;
                        for(var i = 2; i < col; i++)
                        {
                            sheet.Cells[row, i].Formula = "=SUM(" + sheet.Cells[3, i].Address + ":" + sheet.Cells[row-1, i].Address + ")";

                            if (i == colIndex || i - colIndex == 6)
                            {
                                sheet.Cells[row, i].Style.Numberformat.Format = "0%";
                                sheet.Cells[row, i].Formula = "="+ sheet.Cells[row, i - 1].Address + "/" + sheet.Cells[row, i - 3].Address;
                                colIndex = i;
                            }
                            
                            if (i == colIndex + 2)
                            {
                                sheet.Cells[row, i].Style.Numberformat.Format = "0%";
                                sheet.Cells[row, i].Formula = "="+ sheet.Cells[row, i - 1].Address + "/" + sheet.Cells[row, col - 2].Address;
                            }
                        }

                        sheet.Cells[1, 1, row, col-1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[1, 1, row, col-1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[1, 1, row, col-1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[1, 1, row, col-1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin; 

                        sheet.Cells[row, 1, row, col-1].Style.Font.Bold = true;
                        sheet.Cells[row, 1, row, col-1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[row, 1, row, col-1].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                        var thickCol = 2;
                        foreach(var source in masterdata.LeadSources)
                        {
                            sheet.Cells[1, thickCol, row, thickCol+5].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                            thickCol = thickCol + 6;
                        }    
                    });

                    return package.GetAsByteArray();
                }
            }

        }

        public async Task<byte[]> WriteBusinessConversionLeadsToExcelFile(BusinessConversionReportEnvelope BusinessConversionEnvelope)
        {
             using (var stream = new MemoryStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Business Conversion Report");
                    var row = 1;
                    var col = 1;

                    sheet.Cells[row+1, col++].Value = "Team";
                    sheet.Cells[row+1, col++].Value = "Experience Center";

                    await Task.Run(() =>
                    {
                        foreach(var month in BusinessConversionEnvelope.FiscalYear.FiscalMonths)
                        {
                            sheet.Cells[row+1, col].Value = "Conversion Count";
                            sheet.Cells[row+1, col+1].Value = "Conversion Value";
                            sheet.Cells[row, col, row, col+1].Merge = true;
                            sheet.Cells[row, col, row, col+1].Value = month.FiscalMonthLabel;
                            sheet.Cells[row, col, row, col+1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            col+=2;
                        }
                        
                        sheet.Cells[row+1, col].Value = "Conversion Count";
                        sheet.Cells[row+1, col+1].Value = "Conversion Value";
                        sheet.Cells[row, col, row, col+1].Merge = true;
                        sheet.Cells[row, col, row, col+1].Value = BusinessConversionEnvelope.FiscalYear.FiscalYearDuration;
                        sheet.Cells[row, col, row, col+1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        sheet.Cells[row, 1, row+1, col+1].Style.Font.Bold = true;
                        sheet.Cells[row, 1, row+1, col+1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[row, 1, row+1, col+1].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                        row++;

                        var businessConversionLeads = BusinessConversionEnvelope.BusinessConversionLeads;
                        
                        foreach(var businessConversion in businessConversionLeads)
                        {
                            row++;
                            col = 1;
                            sheet.Cells[row, col++].Value = businessConversion.TeamName;
                            sheet.Cells[row, col++].Value = businessConversion.ExperienceCenterShortName;

                            foreach(var leadConversion in businessConversion.LeadConversions)
                            {
                                sheet.Cells[row, col++].Value = leadConversion.ConvertedLeads;
                                sheet.Cells[row, col++].Value = leadConversion.ConversionValue;
                            }
                            
                        }
                        row++;
                        sheet.Cells[row, 1].Value = "Grand Total";
                        for(var i = 3; i < col; i++)
                        {
                            sheet.Cells[row, i].Formula = "=SUM(" + sheet.Cells[3, i].Address + ":" + sheet.Cells[row-1, i].Address + ")";
                        }


                        sheet.Cells[1, 1, row, col-1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[1, 1, row, col-1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[1, 1, row, col-1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[1, 1, row, col-1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin; 

                        sheet.Cells[row, 1, row, col-1].Style.Font.Bold = true;
                        sheet.Cells[row, 1, row, col-1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[row, 1, row, col-1].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                    });
                    
                    return package.GetAsByteArray();
                    
                }
            }
        }
    }
}