using AzureServiceBusLibrary.EventBus.Events;
using System;

namespace AzureServiceBus.Salary.API.Core.IntegrationEvents.Events
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
        public decimal? Salary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="salary"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="modifiedBy"></param>
        public EmployeeAddIntegrationEvent(Guid employeeId, string firstName = null, string lastName = null, decimal? salary = null, DateTime? startDate = null, DateTime? endDate = null, string modifiedBy = null)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            StartDate = startDate;
            EndDate = endDate;
            ModifiedBy = modifiedBy;
        }
    }
}
