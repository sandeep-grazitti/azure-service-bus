﻿namespace AzureServiceBus.Employee.Infrastructure.Configuration.Interfaces
{
    public interface IAzureServiceBusConfiguration
    {
        string ConnectionString { get; set; }
        string SubscriptionClientName { get; set; }
    }
}
