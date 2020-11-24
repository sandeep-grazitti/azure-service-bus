using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBus.Salary.Infrastructure.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
