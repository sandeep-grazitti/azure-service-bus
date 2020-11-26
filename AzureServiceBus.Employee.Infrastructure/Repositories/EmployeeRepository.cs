using AzureServiceBus.Employee.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureServiceBus.Employee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AzureServiceBus.Employee.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _sqlDbContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(EmployeeDbContext sqlDbContext, ILogger<EmployeeRepository> logger)
        {
            _sqlDbContext = sqlDbContext;
            _logger = logger;
        }

        public async Task<Entities.Employee> AddEmployee(Entities.Employee employee)
        {
            //employee.Id = Guid.NewGuid();
            await _sqlDbContext.Employees.AddAsync(employee);
            await _sqlDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(Entities.Employee employee)
        {
            _sqlDbContext.Employees.Remove(employee);
            await _sqlDbContext.SaveChangesAsync();
        }

        public async Task<Entities.Employee> GetByIdAsync(Guid id)
        {
            var employee = await _sqlDbContext.Employees
                .Where(e => e.Id.ToString().ToLower() == id.ToString().ToLower())
                .FirstOrDefaultAsync();
            return employee;

        }

        public async Task UpdateEmployee(Entities.Employee employee)
        {
            _sqlDbContext.Employees.Update(employee);
            await _sqlDbContext.SaveChangesAsync();
        }

        public async Task<IList<Entities.Employee>> ListAllAsync()
        {
            var employees = await _sqlDbContext.Employees
                             .ToListAsync();
            return employees;
        }
    }
}
