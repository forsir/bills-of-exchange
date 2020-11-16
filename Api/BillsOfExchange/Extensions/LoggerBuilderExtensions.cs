using Serilog;
using Serilog.Events;

namespace BillsOfExchange.Extensions
{
    /// <summary>
    /// LoggerBuilder Extensions
    /// </summary>
    public static class LoggerBuilderExtensions
    {
        /// <summary>
        /// Vytváří defaultní konfiguraci loggeru
        /// </summary>
        /// <returns></returns>
        public static ILogger CreateDefaultLogger()
        {
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .WriteTo.Console(outputTemplate: "{Timestamp:dd.M.yy HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Debug(outputTemplate: "{Timestamp:dd.M.yy HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            return logger;
        }
    }
}