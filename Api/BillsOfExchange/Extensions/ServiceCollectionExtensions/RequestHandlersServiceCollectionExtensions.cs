using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BillsOfExchange.Base;
using Microsoft.Extensions.DependencyInjection;

namespace BillsOfExchange.Extensions.ServiceCollectionExtensions
{
    /// <summary>
    /// IAsyncRequestHandler IServiceCollection extensions
    /// </summary>
    public static class RequestHandlersServiceCollectionExtensions
    {
        /// <summary>
        /// Přidá RequestHandlers do IoC
        /// </summary>
        /// <param name="services"><see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</param>
        /// <returns>The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</returns>
        public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            var requestHandlers = getRequestHandlers();
            foreach (var requestHandler in requestHandlers)
            {
                var requestHandlerInterface = requestHandler.FindInterfaces((type, criteria) => type.IsInterface && !type.Name.ToLower().Contains("iasyncrequesthandler"), null).FirstOrDefault();
                if (requestHandlerInterface != default)
                {
                    services.AddScoped(requestHandlerInterface, requestHandler);
                    var lazyGeneric = typeof(Lazy<>).MakeGenericType(requestHandlerInterface);
                    services.AddScoped(lazyGeneric);
                }
            }

            return services;
        }

        /// <summary>
        /// Získá implementované typy requestHandlers v aplikaci
        /// </summary>
        /// <returns></returns>
        private static List<Type> getRequestHandlers()
        {
            List<Type> requestHandlers;
            var requestHandlerInterfaceGen1 = typeof(IAsyncRequestHandler<,>);

            requestHandlers = Assembly.GetAssembly(typeof(Startup)).GetTypes()
                .Where(t =>
                    t.GetInterface(requestHandlerInterfaceGen1.FullName, true) != null && !t.IsInterface
                )
                .ToList();

            return requestHandlers;
        }
    }
}