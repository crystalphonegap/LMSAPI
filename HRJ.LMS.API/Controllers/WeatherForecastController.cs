using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRJ.LMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HRJ.LMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICronIndiaMartLead _cronIndiaMartLead;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHostEnvironment _env;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICronIndiaMartLead cronIndiaMartLead, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _cronIndiaMartLead = cronIndiaMartLead;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetEnvironment")]
        public IActionResult GetEnvironment()
        {
            return Ok(new {
                Staging = _env.IsStaging(),
                Production = _env.IsProduction(),
                Development = _env.IsDevelopment()
            });
        }

        [HttpGet("/testIndiaMart")]
        public IEnumerable<WeatherForecast> GetData()
        {

            _cronIndiaMartLead.PullIndiaMartLeads();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
