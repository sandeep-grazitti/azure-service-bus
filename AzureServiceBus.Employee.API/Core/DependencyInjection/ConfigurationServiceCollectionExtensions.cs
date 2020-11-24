using AzureServiceBus.Employee.Infrastructure.Configuration;
using AzureServiceBus.Employee.Infrastructure.Configuration.Interfaces;
using AzureServiceBus.Employee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AzureServiceBus.Employee.API.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            //services.AddDbContext<EmployeeDbContext>(cfg =>
            //{
            //    cfg.UseSqlServer("Server=.;Database=AzureBusEmpDb;Uid=sa;Pwd=Grazitti$123;");
            //});

            services.Configure<SqlDbDataServiceConfiguration>(config.GetSection("SqlDbSettings"));
            services.AddSingleton<IValidateOptions<SqlDbDataServiceConfiguration>, SqlDbDataServiceConfigurationValidation>();
            services.AddScoped<ISqlDbDataServiceConfiguration, SqlDbDataServiceConfiguration>();
            var sqlDbDataServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<SqlDbDataServiceConfiguration>>().Value;
            services.AddSingleton<ISqlDbDataServiceConfiguration>(sqlDbDataServiceConfiguration);

            //services.Configure<AzureServiceBusConfiguration>(config.GetSection("AzureServiceBusSettings"));
            services.AddSingleton<IValidateOptions<AzureServiceBusConfiguration>, AzureServiceBusConfigurationValidation>();
            services.AddScoped<IAzureServiceBusConfiguration, AzureServiceBusConfiguration>();
            //var azureServiceBusConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureServiceBusConfiguration>>().Value;
            //services.AddSingleton<IAzureServiceBusConfiguration>(azureServiceBusConfiguration);

            return services;
        }
    }
}
