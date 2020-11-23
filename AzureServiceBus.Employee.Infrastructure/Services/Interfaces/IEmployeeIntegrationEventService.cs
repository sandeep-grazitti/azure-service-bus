using AzureServiceBusLibrary.EventBus.Events;
using System.Threading.Tasks;

namespace AzureServiceBus.Employee.Infrastructure.Services.Interfaces
{
    public interface IEmployeeIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(IntegrationEvent @event);
        Task AddAndSaveEventAsync(IntegrationEvent @event);
    }
}
