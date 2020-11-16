using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using BillsOfExchange.Attributes;
using BillsOfExchange.Extensions;
using BillsOfExchange.Extensions.ServiceCollectionExtensions;
using BillsOfExchange.Middlewares;
using BillsOfExchange.Providers;
using BillsOfExchange.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using SimpleProxy.Extensions;

namespace BillsOfExchange
{
    /// <summary>
    /// Konfigurace Aplikace
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Konfigurace
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Environment
        /// </summary>
        public IWebHostEnvironment Env { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.Configuration = configuration;
            this.Env = env;
        }

        /// <summary>
        /// Konfigurace IoC
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = ctx => new BadRequestObjectResult(new ValidationProblemDetails(ctx.ModelState)
                {
                    Title = "Chyba validace"
                });
            });
            ;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("latest", new OpenApiInfo()
                {
                    Title = $"{AppDomain.CurrentDomain.FriendlyName} - {this.Env?.EnvironmentName ?? "unversioned"}",
                    Version = Assembly.GetAssembly(typeof(Startup)).GetName().Version.ToString()
                });

                var xmlFile = $"{Assembly.GetAssembly(typeof(Startup)).GetName().Name}.xml";

                if (File.Exists(xmlFile))
                {
                    options.IncludeXmlComments(xmlFile);
                }


                options.DocInclusionPredicate((docName, description) => true);
                options.EnableAnnotations();
            });

            services.AddSingleton<IDataSourceProvider, DataSourceProvider>();


            services.AddQueries();
            services.AddMappers();
            services.AddRequestHandlers();
            services.AddValidators();

            services.AddSingleton<IFileProvider, JsonDataFilesProvider>();

            services.EnableSimpleProxy(p => p.AddInterceptor<LogMethodAttribute, LogMethodInterceptor>());

            services.AddScopedWithProxy<IBillOfExchangeRepository, BillOfExchangeRepository>();
            services.AddScopedWithProxy<IPartyRepository, PartyRepository>();

            services.AddDefaultCors();
        }

        /// <summary>
        /// Konfigurace builderu
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var handler = new ExceptionProblemDetailHandler();
                    await handler.Invoke(context);
                });
            });
            app.UseRouting();
            app.UseMiddleware<ResponseCacheMiddleware>();
            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint($"latest/swagger.json", AppDomain.CurrentDomain.FriendlyName); });
        }
    }
}