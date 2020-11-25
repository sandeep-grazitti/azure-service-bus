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
        Task<Entities.Employee> AddEmployee(Entities.Employee employee);
        Task UpdateEmployee(Entities.Employee employee);
        Task DeleteEmployee(Entities.Employee employee);
    }
}
