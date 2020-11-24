﻿using System;

namespace AzureServiceBus.Salary.Infrastructure.Entities
{
    public class EmployeeSalary// : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Salary { get; set; }
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
