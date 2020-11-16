using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BillsOfExchange.Base;
using Microsoft.Extensions.DependencyInjection;

namespace BillsOfExchange.Extensions.ServiceCollectionExtensions
{
    /// <summary>
    /// IValidator IServiceCollection extensions
    /// </summary>
    public static class ValidatorsServiceCollectionExtensions
    {
        /// <summary>
        /// Přidá IValidator do IoC
        /// </summary>
        /// <param name="services"><see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</param>
        /// <returns>The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            var validators = getValidators();
            foreach (var validator in validators)
            {
                var validatorInterface = validator.FindInterfaces((type, criteria) => type.IsInterface && type.Name.ToLower().Contains("ivalidator"), null).FirstOrDefault();
                if (validatorInterface != default)
                {
                    services.AddSingleton(validatorInterface, validator);
                }
            }

            return services;
        }

        /// <summary>
        /// Získá implementované typy IValidator v aplikaci
        /// </summary>
        /// <returns></returns>
        private static List<Type> getValidators()
        {
            List<Type> validators;
            var validatorInterface = typeof(IValidator<>);

            validators = Assembly.GetAssembly(typeof(Startup)).GetTypes()
                .Where(t =>
                    t.GetInterface(validatorInterface.FullName, true) != null && !t.IsInterface
                )
                .ToList();

            return validators;
        }
    }
}