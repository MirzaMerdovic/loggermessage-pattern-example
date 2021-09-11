using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace LoggerConsole
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            IConfigurationRoot configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

            var host = new HostBuilder()
                .ConfigureAppConfiguration(x =>
                {
                    x.AddConfiguration(configuration);
                })
                .ConfigureLogging((ctx, builder) =>
                {
                    builder.SetMinimumLevel(LogLevel.Debug);
                    builder.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Warning);
                    builder.AddJsonConsole(x =>
                            {
                                x.IncludeScopes = true;
                                x.UseUtcTimestamp = true;
                                x.JsonWriterOptions = new JsonWriterOptions { Indented = true };
                            });
                })
                .ConfigureServices((ctx, builder) =>
                {
                    builder.AddHostedService<LoggerHost>();
                })
                .Build();

            host.Run();
        }
    }
}
