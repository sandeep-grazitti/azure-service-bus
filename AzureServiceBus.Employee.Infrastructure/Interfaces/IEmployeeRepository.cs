using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureServiceBus.Employee.Infrastructure.Entities;

namespace AzureServiceBus.Employee.Infrastructure.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Entities.Employee> GetByIdAsync(Guid id);
        Task<IList<Entities.Employee>> ListAllAsync();
        Entities.Employee Add(Entities.Employee employee);
        void Update(Entities.Employee employee);
        void Delete(Entities.Employee employee);
    }
}
