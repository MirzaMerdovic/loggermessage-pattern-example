using System.Text.Json;
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
            var host = Host.CreateDefaultBuilder()
                // Uncomment if you don't want to use appsettings.json configuration
                //.ConfigureLogging((ctx, builder) =>
                //{
                //    builder.SetMinimumLevel(LogLevel.Debug);
                //    builder.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Warning);
                //    builder.AddJsonConsole(x =>
                //            {
                //                x.IncludeScopes = true;
                //                x.UseUtcTimestamp = true;
                //                x.JsonWriterOptions = new JsonWriterOptions { Indented = true };
                //            });
                //})
                .ConfigureServices((ctx, builder) =>
                {
                    builder.AddHostedService<LoggerHost>();
                })
                .Build();

            host.Run();
        }
    }
}
