using AzureServiceBusLibrary.EventBus.Events;
using System;

namespace AzureServiceBus.Salary.API.Core.IntegrationEvents.Events
{
    public class EmployeeAddIntegrationEvent : IntegrationEvent
    {
        public Guid EmployeeId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public EmployeeAddIntegrationEvent(Guid employeeId, string firstName = null, string lastName = null, decimal? salary = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
