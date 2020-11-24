using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using AzureServiceBus.Employee.Infrastructure.Data;
using AzureServiceBus.Employee.Infrastructure.Services.Interfaces;
using AzureServiceBusLibrary.EventBus.Events;
using AzureServiceBusLibrary.EventBus.Events.Interfaces;
using AzureServiceBusLibrary.EventLog;
using AzureServiceBusLibrary.EventLog.Services.Interfaces;

namespace AzureServiceBus.Employee.Infrastructure.Services
{
    public class EmployeeIntegrationEventService : IEmployeeIntegrationEventService
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly IEventBus _eventBus;
        private readonly IEventLogService _eventLogService;
        private readonly ILogger<EmployeeIntegrationEventService> _logger;
        private readonly Func<DbConnection, IEventLogService> _integrationEventLogServiceFactory;

        public EmployeeIntegrationEventService(EmployeeDbContext employeeDbContext, Func<DbConnection, IEventLogService> integrationEventLogServiceFactory,
                                     IEventBus eventBus,
                                     ILogger<EmployeeIntegrationEventService> logger)
        {
            _employeeDbContext = employeeDbContext ?? throw new ArgumentNullException(nameof(employeeDbContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_employeeDbContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent @event)
        {
            await ResilientTransaction.CreateNew(_employeeDbContext).ExecuteAsync(async () =>
            {
                await _employeeDbContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(@event, _employeeDbContext.Database.CurrentTransaction);
            });
        }

        public async Task PublishEventsThroughEventBusAsync(IntegrationEvent @event)
        {
            try
            {
                //await _eventLogService.MarkEventAsInProgressAsync(@event.Id);
                await _eventBus.PublishAsync(@event);
               // await _eventLogService.MarkEventAsPublishedAsync(@event.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR publishing integration event: '{IntegrationEventId}'", @event.Id);

                await _eventLogService.MarkEventAsFailedAsync(@event.Id);
            }
        }
    }
}
