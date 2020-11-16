using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace BillsOfExchange.Extensions
{
    /// <summary>
    /// Cors Extensions
    /// </summary>
    public static class CorsExtensions
    {
        private static readonly List<string> allowedOrigins = new List<string>()
        {
            "http://localhost",
            "https://localhost",
            "http://localhost:5000",
            "https://localhost:5000",
            "http://localhost:4200",
            "https://localhost:4200"
        };

        /// <summary>
        /// Přidá základní nastavení CORS
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(config =>
                {
                    config.WithOrigins(allowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .WithMethods("GET", "OPTIONS");
                });
            });

            return services;
        }
    }
}