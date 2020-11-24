using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureServiceBus.Salary.Infrastructure.Data;
using AzureServiceBus.Salary.Infrastructure.Entities;
using AzureServiceBus.Salary.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AzureServiceBus.Salary.Infrastructure.Repositories
{
    public class EmployeeSalaryRepository : IEmployeeSalaryRepository
    {
        private readonly ILogger<EmployeeSalaryRepository> _logger;
        private readonly EmployeeSalaryDbContext _sqlDbContext;

        public EmployeeSalaryRepository(EmployeeSalaryDbContext sqlDbContext, ILogger<EmployeeSalaryRepository> logger)
        {
            _logger = logger;
            _sqlDbContext = sqlDbContext;
        }

        public void DeleteEmployeeSalary(EmployeeSalary employeeSalary)
        {
            _sqlDbContext.EmployeeSalaries.Remove(employeeSalary);
        }

        public async Task<EmployeeSalary> GetEmployeeSalaryAsync(string employeeId)
        {
            var salaryByEmployee =
                await _sqlDbContext.EmployeeSalaries.FirstOrDefaultAsync(x => x.EmployeeId.ToString() == employeeId);

            return salaryByEmployee;
        }

        public async Task<EmployeeSalary> UpdateEmployeeSalaryAsync(EmployeeSalary empSalary)
        {
            var salaryByEmployee =
                await _sqlDbContext.EmployeeSalaries.FirstOrDefaultAsync(x => x.EmployeeId == empSalary.EmployeeId);

            if (salaryByEmployee == null)
            {
                _logger.LogInformation("Problem occured while retrieving salary!");
                return null;
            }

            _logger.LogInformation("Employee salary retrieved successfully.");
            return await GetEmployeeSalaryAsync(empSalary.EmployeeId.ToString());
        }

        public async Task<IList<Employee>> GetEmployees()
        {
            return await _sqlDbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeSalary> AddEmployeeSalaryAsync(EmployeeSalary empSalary)
        {
            _logger.LogInformation("Employee salary created successfully.");
            await _sqlDbContext.EmployeeSalaries.AddAsync(empSalary);
            return empSalary;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _logger.LogInformation("Employee created successfully.");
            await _sqlDbContext.Employees.AddAsync(employee);
            var count = await _sqlDbContext.SaveChangesAsync();
            return employee;
        }

    }
}
