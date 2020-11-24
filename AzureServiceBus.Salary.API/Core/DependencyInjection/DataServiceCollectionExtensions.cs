using AzureServiceBus.Salary.Infrastructure.Configuration.Interfaces;
using AzureServiceBus.Salary.Infrastructure.Data;
using AzureServiceBusLibrary.EventLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using AzureServiceBus.Salary.Infrastructure.Interfaces;
using AzureServiceBus.Salary.Infrastructure.Repositories;

namespace AzureServiceBus.Salary.API.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var sqlDbConfiguration = serviceProvider.GetRequiredService<ISqlDbDataServiceConfiguration>();

            services.AddDbContext<EmployeeSalaryDbContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(EmployeeSalaryDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });


            services.AddDbContext<EventLogContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(EmployeeSalaryDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });

            services.AddScoped<IEmployeeSalaryRepository, EmployeeSalaryRepository>();

            return services;
        }
    }
}
