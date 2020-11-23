using AzureServiceBus.Employee.API.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AzureServiceBus.Employee.API.Core.DependencyInjection
{
    public static class ModelValidationServiceCollectionExtensions
    {
        public static IServiceCollection AddModelValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Infrastructure.Entities.Employee>, EmployeeValidator>();
            return services;
        }
    }
}
