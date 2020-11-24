using AzureServiceBusLibrary.EventBus.Events;
using System;

namespace AzureServiceBus.Salary.API.Core.IntegrationEvents.Events
{
    public class EmployeeChangedIntegrationEvent : IntegrationEvent
    {
        public Guid EmployeeId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string EmpCode { get; set; }
        public bool IsActive { get; set; }
        public decimal OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public EmployeeChangedIntegrationEvent(Guid employeeId, string firstName = null, string lastName = null,
            string address = null, string contact = null, string departmentName = null, DateTime? joiningDate = null,
            string empCode = null, bool? isActive = null)
        {
            EmployeeId = employeeId;

            if (!string.IsNullOrEmpty(firstName))
                FirstName = firstName;

            if (!string.IsNullOrEmpty(lastName))
                LastName = lastName;

            if (!string.IsNullOrEmpty(address))
                Address = address;

            if (!string.IsNullOrEmpty(contact))
                Contact = contact;

            if (!string.IsNullOrEmpty(departmentName))
                DepartmentName = departmentName;

            if (joiningDate.HasValue && joiningDate.Value != DateTime.MinValue)
                JoiningDate = joiningDate;

            if (!string.IsNullOrEmpty(empCode))
                EmpCode = empCode;

            if (isActive.HasValue)
                IsActive = isActive.Value;
        }
    }
}
