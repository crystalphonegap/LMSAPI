using System;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HRJ.LMS.API.Schedulers
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer _timer;
        private ICronIndiaMartLead cronIndiaMartLead;
        private IServiceScope scope;
        private readonly IHostEnvironment _env;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory serviceScopeFactory, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            if (_env.IsProduction())
            {
                _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(16)); //Production time schedule
            }
            else if (_env.IsStaging())
            {
                _timer = new Timer(DoWork, null, TimeSpan.Zero,
                    TimeSpan.FromHours(6));  //QA time schedule
            }
            else if (_env.IsDevelopment())
            {
                Console.WriteLine("Developement");
            }
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            if (scope == null)
            {
                scope = _serviceScopeFactory.CreateScope();
            }

            if (cronIndiaMartLead == null)
            {
                cronIndiaMartLead = scope.ServiceProvider.GetRequiredService<ICronIndiaMartLead>();
            }

            cronIndiaMartLead.PullIndiaMartLeads();
            /* _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}, {1} {2}", count, cronEditLRSAP.GetHashCode(), scope.GetHashCode()); */
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}