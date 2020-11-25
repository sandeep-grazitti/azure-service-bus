using System;
using AzureServiceBusLibrary.EventBus.Events;

namespace AzureServiceBus.Employee.API.Core.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeChangedIntegrationEvent : IntegrationEvent
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
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JoiningDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmpCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal NewSalary { get; set; }
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
        /// <param name="address"></param>
        /// <param name="contact"></param>
        /// <param name="departmentName"></param>
        /// <param name="joiningDate"></param>
        /// <param name="empCode"></param>
        /// <param name="isActive"></param>
        /// <param name="salary"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="modifiedBy"></param>
        public EmployeeChangedIntegrationEvent(Guid employeeId, string firstName = null, string lastName = null,
            string address = null, string contact = null, string departmentName = null, DateTime? joiningDate = null,
            string empCode = null, bool? isActive = null, decimal? salary = null, DateTime? startDate = null, DateTime? endDate = null, string modifiedBy = null)
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

            if (salary.HasValue && salary != decimal.Zero)
                NewSalary = salary.Value;

            if (startDate.HasValue && startDate.Value != DateTime.MinValue)
                StartDate = startDate.Value;

            if (endDate.HasValue && endDate.Value != DateTime.MinValue)
                EndDate = endDate.Value;

            if (!string.IsNullOrEmpty(modifiedBy))
                ModifiedBy = modifiedBy;
        }
    }
}
