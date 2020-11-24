using System;
using AzureServiceBusLibrary.EventBus.Events;

namespace AzureServiceBus.Employee.API.Core.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeAddIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public EmployeeAddIntegrationEvent(Guid employeeId, string firstName = null, string lastName = null)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
