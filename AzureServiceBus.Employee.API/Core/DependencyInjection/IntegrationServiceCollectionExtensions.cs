using AzureServiceBus.Employee.Infrastructure.Configuration.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using AzureServiceBus.Employee.Infrastructure.Services;
using AzureServiceBus.Employee.Infrastructure.Services.Interfaces;
using AzureServiceBusLibrary.EventBus;
using AzureServiceBusLibrary.EventBus.Events.Interfaces;
using AzureServiceBusLibrary.EventBus.Services;
using AzureServiceBusLibrary.EventBus.Services.Interfaces;
using AzureServiceBusLibrary.EventLog.Services;
using AzureServiceBusLibrary.EventLog.Services.Interfaces;

namespace AzureServiceBus.Employee.API.Core.DependencyInjection
{
    public static class IntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var azureServiceBusConfiguration = serviceProvider.GetRequiredService<IAzureServiceBusConfiguration>();

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<IEmployeeIntegrationEventService, EmployeeIntegrationEventService>();

            services.AddSingleton<IServiceBusConnectionManagementService>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ServiceBusConnectionManagementService>>();
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(azureServiceBusConfiguration.ConnectionString);
                return new ServiceBusConnectionManagementService(logger, serviceBusConnection);
            });

            services.AddSingleton<IEventBus, AzureServiceBusEventBus>(sp =>
            {
                var serviceBusConnectionManagementService = sp.GetRequiredService<IServiceBusConnectionManagementService>();
                var logger = sp.GetRequiredService<ILogger<AzureServiceBusEventBus>>();
                var eventBusSubscriptionManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var eventBus = new AzureServiceBusEventBus(serviceBusConnectionManagementService, eventBusSubscriptionManager,
                    serviceProvider, logger, azureServiceBusConfiguration.SubscriptionClientName);
                eventBus.SetupAsync().GetAwaiter().GetResult();

                return eventBus;
            });

            services.AddTransient<Func<DbConnection, IEventLogService>>(
                    sp => (DbConnection connection) => new EventLogService(connection));

            return services;
        }
    }
}
