using Microsoft.Azure.ServiceBus;

namespace AzureServiceBusLibrary.EventBus.Services.Interfaces
{
    public interface IServiceBusConnectionManagementService
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient CreateTopicClient();
    }
}
