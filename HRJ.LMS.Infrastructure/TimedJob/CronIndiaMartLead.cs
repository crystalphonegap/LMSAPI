using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HRJ.LMS.Infrastructure.TimedJob
{
    public class CronIndiaMartLead : ICronIndiaMartLead
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILeadActivityLog _leadActivityLog;

        public CronIndiaMartLead(AppDbContext context, IConfiguration config, IMapper mapper, ILeadActivityLog leadActivityLog)
        {
            _leadActivityLog = leadActivityLog;
            _mapper = mapper;
            _config = config;
            _context = context;
        }
        public async void PullIndiaMartLeads()
        {
            //throw new System.NotImplementedException();

            var maxLeadSourceStartTime = await _context.Leads
                                    .Where(x => x.LeadSource.Equals("India Mart"))
                                    .MaxAsync(x => (DateTime?)x.LeadDateTime);

            var CurrentDateTime = DateTime.Now;
            var leadSourceEndTime = DateTime.Now;

            if (maxLeadSourceStartTime == null)
            {
                maxLeadSourceStartTime = new DateTime(CurrentDateTime.Year, CurrentDateTime.Month, 1, 0, 0, 0);

                leadSourceEndTime = maxLeadSourceStartTime.GetValueOrDefault().AddDays(5).AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (CurrentDateTime.Subtract(maxLeadSourceStartTime.GetValueOrDefault()).Days >= 5)
            {
                leadSourceEndTime = new DateTime(maxLeadSourceStartTime.GetValueOrDefault().Year,
                                        maxLeadSourceStartTime.GetValueOrDefault().Month,
                                         maxLeadSourceStartTime.GetValueOrDefault().Day, 23, 59, 59);

                leadSourceEndTime = leadSourceEndTime.AddDays(5);
                maxLeadSourceStartTime = maxLeadSourceStartTime.GetValueOrDefault().AddSeconds(1);
            }

            var httpClient = new HttpClient();

            var apiBaseURL = _config.GetSection("AppSettings:IndiaMartAPI").Value;

            var apiURLWithParameters = string.Format(apiBaseURL,
                        maxLeadSourceStartTime.GetValueOrDefault().ToString("dd-MMM-yyyy HH:mm:ss"),
                        leadSourceEndTime.ToString("dd-MMM-yyyy HH:mm:ss"));

            Console.WriteLine(apiURLWithParameters);
            var httpResponse = await httpClient.GetAsync(apiURLWithParameters);

            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    //response error handling to be implemented

                    var strLeadData = await httpResponse.Content.ReadAsStreamAsync();

                    //converting JSON string to JSON object
                    var leadData = await System.Text.Json.JsonSerializer.DeserializeAsync<List<LeadIndiaMartDto>>(strLeadData);

                    //mapping the source lead from India Mart to LeadCapture DTO
                    var leadDtoList = _mapper.Map<List<LeadIndiaMartDto>, List<LeadCaptureDto>>(leadData);

                    //mapping the source lead from LeadCapture DTO to Lead
                    var leads = _mapper.Map<List<LeadCaptureDto>, List<Lead>>(leadDtoList);

                    //all leads are added to the system - not updating existing if any

                    //removing existing leads from source
                    var leadSourceIds = leads.Select(x => x.LeadSourceId);
                    var dbLeads = _context.Leads
                                    .Where(x => leadSourceIds.Contains(x.LeadSourceId) && x.LeadSource == "India Mart")
                                    .Select(x => x.LeadSourceId);

                    var sourceLeads = leads.RemoveAll(x => dbLeads.Contains(x.LeadSourceId));

                    await _context.Leads.AddRangeAsync(leads);
                    await _context.SaveChangesAsync();

                    await _leadActivityLog.AddRangeLeadActivityLog(leads, "System", "Start", "Lead Added in the system");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }

        }
    }
}