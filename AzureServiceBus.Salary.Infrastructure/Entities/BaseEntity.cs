using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBus.Salary.Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
