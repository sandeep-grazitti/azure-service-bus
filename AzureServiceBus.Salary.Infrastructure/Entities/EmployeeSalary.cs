using System;

namespace AzureServiceBus.Salary.Infrastructure.Entities
{
    public class EmployeeSalary
    {
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Salary { get; set; }

        public EmployeeSalary(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
