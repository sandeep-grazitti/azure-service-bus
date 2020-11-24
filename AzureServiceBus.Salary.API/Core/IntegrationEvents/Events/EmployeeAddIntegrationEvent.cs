﻿using AzureServiceBusLibrary.EventBus.Events;
using System;

namespace AzureServiceBus.Salary.API.Core.IntegrationEvents.Events
{
    public class EmployeeAddIntegrationEvent : IntegrationEvent
    {
        public Guid EmployeeId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EmployeeAddIntegrationEvent(Guid employeeId, string firstName = null, string lastName = null)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
