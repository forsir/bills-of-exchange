using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Constants;
using BillsOfExchange.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BillsOfExchange
{
    /// <summary>
    /// Zavadìè
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Vstup
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("cs-CZ");
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("cs-CZ");

            Environment.SetEnvironmentVariable(EnvironmentKeys.UsePollingFileWatcher, "1");

            Log.Logger = LoggerBuilderExtensions.CreateDefaultLogger();

            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, "Chyba startu aplikace");
            }
            finally
            {
                Log.Information("Ukonèování aplikace");
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Vytvoøení hosta
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(KestrelExtensions.DefaultKestrelConfig());
                });
    }
}