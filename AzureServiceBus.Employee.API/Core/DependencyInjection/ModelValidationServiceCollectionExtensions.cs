using AzureServiceBus.Employee.API.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AzureServiceBus.Employee.API.Core.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelValidationServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddModelValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Infrastructure.Entities.Employee>, EmployeeValidator>();
            return services;
        }
    }
}
