using AzureServiceBus.Employee.Infrastructure.Configuration.Interfaces;
using AzureServiceBus.Employee.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using AzureServiceBus.Employee.Infrastructure.Data;
using AzureServiceBus.Employee.Infrastructure.Interfaces;
using AzureServiceBusLibrary.EventLog;

namespace AzureServiceBus.Employee.API.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var sqlDbConfiguration = serviceProvider.GetRequiredService<ISqlDbDataServiceConfiguration>();

            services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(EmployeeDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });


            services.AddDbContext<EventLogContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(EmployeeDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
