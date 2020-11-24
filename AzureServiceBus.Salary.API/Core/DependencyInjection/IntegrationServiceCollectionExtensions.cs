using AzureServiceBus.Salary.API.Core.IntegrationEvents.EventHandlers;
using AzureServiceBus.Salary.API.Core.IntegrationEvents.Events;
using AzureServiceBus.Salary.Infrastructure.Configuration.Interfaces;
using AzureServiceBusLibrary.EventBus;
using AzureServiceBusLibrary.EventBus.Events.Interfaces;
using AzureServiceBusLibrary.EventBus.Services;
using AzureServiceBusLibrary.EventBus.Services.Interfaces;
using AzureServiceBusLibrary.EventLog.Services;
using AzureServiceBusLibrary.EventLog.Services.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace AzureServiceBus.Salary.API.Core.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class IntegrationServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var azureServiceBusConfiguration = serviceProvider.GetRequiredService<IAzureServiceBusConfiguration>();

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<IIntegrationEventHandler<EmployeeChangedIntegrationEvent>, EmployeeSalaryChangedIntegrationEventHandler>();
            services.AddTransient<IIntegrationEventHandler<EmployeeAddIntegrationEvent>, EmployeeAddedIntegrationEventHandler>();

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
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var eventBus = new AzureServiceBusEventBus(serviceBusConnectionManagementService, eventBusSubcriptionsManager,
                    serviceProvider, logger, azureServiceBusConfiguration.SubscriptionClientName);
                return eventBus;
            });


            services.AddTransient<Func<DbConnection, IEventLogService>>(
                    sp => (DbConnection connection) => new EventLogService(connection));

            serviceProvider = services.BuildServiceProvider();

            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.SetupAsync().GetAwaiter().GetResult();
            eventBus.SubscribeAsync<EmployeeChangedIntegrationEvent,
                                    IIntegrationEventHandler<EmployeeChangedIntegrationEvent>>()
                                    .GetAwaiter().GetResult();
            eventBus.SubscribeAsync<EmployeeAddIntegrationEvent,
                    IIntegrationEventHandler<EmployeeAddIntegrationEvent>>()
                .GetAwaiter().GetResult();

            return services;
        }
    }
}
