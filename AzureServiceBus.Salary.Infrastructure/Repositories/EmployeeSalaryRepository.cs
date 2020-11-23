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

        public async Task<EmployeeSalary> UpdateEmployeeSalaryAsync(EmployeeSalary reservation)
        {
            var salaryByEmployee =
                await _sqlDbContext.EmployeeSalaries.FirstOrDefaultAsync(x => x.EmployeeId == reservation.EmployeeId);

            if (salaryByEmployee == null)
            {
                _logger.LogInformation("Problem occured while retrieving salary!");
                return null;
            }

            _logger.LogInformation("Employee salary retrieved successfully.");
            return await GetEmployeeSalaryAsync(reservation.EmployeeId.ToString());
        }

    }
}
