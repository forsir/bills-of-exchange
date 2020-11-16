using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BillsOfExchange.Base;
using Microsoft.Extensions.DependencyInjection;

namespace BillsOfExchange.Extensions.ServiceCollectionExtensions
{
    /// <summary>
    /// IMapper IServiceCollection extensions
    /// </summary>
    public static class MapperServiceCollectionExtensions
    {
        /// <summary>
        /// Přidá mappery do IoC
        /// </summary>
        /// <param name="services"><see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</param>
        /// <returns>The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</returns>
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            var mappers = getMappers();
            foreach (var mapper in mappers)
            {
                var mapperInterface = mapper.FindInterfaces((type, criteria) => type.IsInterface && type.Name.ToLower().Contains("imapper"), null).FirstOrDefault();
                if (mapperInterface != default)
                {
                    services.AddSingleton(mapperInterface, mapper);
                }
            }

            return services;
        }

        /// <summary>
        /// Získá implementované typy mapperů v aplikaci
        /// </summary>
        /// <returns></returns>
        private static List<Type> getMappers()
        {
            List<Type> mappers;
            var mapperInterface = typeof(IMapper<,>);

            mappers = Assembly.GetAssembly(typeof(Startup)).GetTypes()
                .Where(t => t.GetInterface(mapperInterface.FullName, true) != null)
                .ToList();

            return mappers;
        }
    }
}