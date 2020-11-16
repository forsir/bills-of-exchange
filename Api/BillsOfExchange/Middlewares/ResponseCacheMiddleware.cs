using System;
using System.Net;
using System.Threading.Tasks;
using BillsOfExchange.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace BillsOfExchange.Middlewares
{
    /// <summary>
    /// Middleware pro response caching
    /// </summary>
    public class ResponseCacheMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="next"></param>
        public ResponseCacheMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Middleware invoke
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            // disable non-rest requests (like swagger files)
            if (!context.Request.RouteValues.ContainsKey("controller"))
            {
                return next(context);
            }

            var jsonDataFilesProvider = context.RequestServices.GetService<IFileProvider>() as JsonDataFilesProvider;

            if (context.Request.GetTypedHeaders().IfModifiedSince.HasValue)
            {
                var lastModifiedTicks = jsonDataFilesProvider.MaxFilesModified;

                var modifiedSince = context.Request.GetTypedHeaders().IfModifiedSince.Value;

                if (modifiedSince.Ticks == lastModifiedTicks)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.NotModified;
                    return Task.CompletedTask;
                }
            }

            context.Response.GetTypedHeaders().CacheControl =
                new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    MustRevalidate = true
                };

            var lastModified = new DateTimeOffset().AddTicks(jsonDataFilesProvider.MaxFilesModified);
            context.Response.GetTypedHeaders().LastModified = lastModified;

            return next(context);
        }
    }
}