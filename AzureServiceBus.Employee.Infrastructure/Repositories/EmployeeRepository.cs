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

        public Entities.Employee Add(Entities.Employee employee)
        {
            employee.Id = Guid.NewGuid();
            return _sqlDbContext.Employees.Add(employee).Entity;
        }

        public void Delete(Entities.Employee employee)
        {
            _sqlDbContext.Employees.Remove(employee);
        }

        public async Task<Entities.Employee> GetByIdAsync(Guid id)
        {
            var employee = await _sqlDbContext.Employees
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();
            return employee;

        }

        public void Update(Entities.Employee employee)
        {
            _sqlDbContext.Update(_sqlDbContext);
        }

        public async Task<IList<Entities.Employee>> ListAllAsync()
        {
            var employees = await _sqlDbContext.Employees
                             .ToListAsync();
            return employees;
        }
    }
}
