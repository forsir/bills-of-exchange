using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BillsOfExchange.Base;
using Microsoft.Extensions.DependencyInjection;

namespace BillsOfExchange.Extensions.ServiceCollectionExtensions
{
    /// <summary>
    /// IAsyncQuery IServiceCollection extensions
    /// </summary>
    public static class QueriesServiceCollectionExtensions
    {
        /// <summary>
        /// Přidá Queries do IoC
        /// </summary>
        /// <param name="services"><see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</param>
        /// <returns>The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</returns>
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            var queries = getQueries();
            foreach (var query in queries)
            {
                var queryInterface = query.FindInterfaces((type, criteria) => type.IsInterface && !type.Name.ToLower().Contains("iasyncquery"), null).FirstOrDefault();
                if (queryInterface != default)
                {
                    services.AddScoped(queryInterface, query);
                }
            }

            return services;
        }

        /// <summary>
        /// Získá implementované typy queries v aplikaci
        /// </summary>
        /// <returns></returns>
        private static List<Type> getQueries()
        {
            List<Type> queries;
            var queryInterfaceGen1 = typeof(IAsyncQuery<,>);
            var queryInterfaceGen2 = typeof(IAsyncQuery<,,>);

            queries = Assembly.GetAssembly(typeof(Startup)).GetTypes()
                .Where(t =>
                    (t.GetInterface(queryInterfaceGen1.FullName, true) != null ||
                     t.GetInterface(queryInterfaceGen2.FullName, true) != null) &&
                    !t.IsInterface
                )
                .ToList();

            return queries;
        }
    }
}