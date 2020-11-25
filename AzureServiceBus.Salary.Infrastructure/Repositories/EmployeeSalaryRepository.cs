using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task DeleteEmployeeSalary(EmployeeSalary employeeSalary)
        {
            _sqlDbContext.EmployeeSalaries.Remove(employeeSalary);
            await _sqlDbContext.SaveChangesAsync();
        }

        public async Task<EmployeeSalary> GetEmployeeSalaryAsync(string employeeId)
        {
            var salaryByEmployee =
                await _sqlDbContext.EmployeeSalaries.FirstOrDefaultAsync(x =>
                    x.EmployeeId.ToString().ToLower() == employeeId.ToString().ToLower());

            return salaryByEmployee;
        }

        public async Task UpdateEmployeeSalaryAsync(EmployeeSalary empSalary)
        {
            var salaryByEmployee =
                await _sqlDbContext.EmployeeSalaries.FirstOrDefaultAsync(x =>
                    x.EmployeeId.ToString().ToLower() == empSalary.EmployeeId.ToString().ToLower());

            if (salaryByEmployee == null)
            {
                _logger.LogInformation("Problem occured while retrieving salary!");
            }
            else
            {
                _logger.LogInformation("Employee salary retrieved successfully.");
                salaryByEmployee.EmployeeId = empSalary.EmployeeId;
                salaryByEmployee.Salary = empSalary.Salary;
                salaryByEmployee.StartDate = empSalary.StartDate;
                salaryByEmployee.EndDate = empSalary.EndDate;
                salaryByEmployee.ModifiedBy = "";
                salaryByEmployee.ModifiedOn = empSalary.ModifiedOn;
                _sqlDbContext.EmployeeSalaries.Update(salaryByEmployee);
                await _sqlDbContext.SaveChangesAsync();
            }
        }

        public async Task<IList<Employee>> GetEmployees()
        {
            return await _sqlDbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeSalary> AddEmployeeSalaryAsync(EmployeeSalary empSalary)
        {
            _logger.LogInformation("Employee salary created successfully.");
            await _sqlDbContext.EmployeeSalaries.AddAsync(empSalary);
            await _sqlDbContext.SaveChangesAsync();
            return empSalary;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _logger.LogInformation("Employee created successfully.");
            await _sqlDbContext.Employees.AddAsync(employee);
            await _sqlDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Entities.Employee> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _sqlDbContext.Employees
                .Where(e => e.EmployeeId.ToString().ToLower() == id.ToString().ToLower())
                .FirstOrDefaultAsync();
            return employee;

        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee =
                await _sqlDbContext.Employees.FirstOrDefaultAsync(x =>
                    x.EmployeeId.ToString().ToLower() == employee.EmployeeId.ToString().ToLower());

            if (existingEmployee == null)
            {
                _logger.LogInformation("Problem occured while retrieving salary!");
            }
            else
            {
                _logger.LogInformation("Employee retrieved successfully.");

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                _sqlDbContext.Employees.Update(existingEmployee);
                await _sqlDbContext.SaveChangesAsync();
            }
        }
    }
}
