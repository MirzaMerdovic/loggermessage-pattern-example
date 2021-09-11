using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoggerConsole
{
    internal class LoggerHost : IHostedService
    {
        private readonly ILogger _logger;

        public LoggerHost(ILogger<LoggerHost> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var _ = _logger.AppendMethodScope("LoggerHost.StartAsync");
            _logger.HostIsRunning();

            var random = new Random().Next(100);
            using var numberAppender = _logger.AppendNumber(random);
            _logger.ReceivedRandomNumber();

            var state = random % 2 == 0;
            using var stateAppender = _logger.AppendState(state ? "even" : "odd");
            _logger.CalculatedState();

            if (state && random < 25)
            {
                _logger.RunningStateCritical();
                throw new ApplicationException("Force stop initialized");
            }

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
