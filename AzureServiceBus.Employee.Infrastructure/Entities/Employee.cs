using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBus.Employee.Infrastructure.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string EmpCode { get; set; }
        public bool IsActive { get; set; }
    }
}
