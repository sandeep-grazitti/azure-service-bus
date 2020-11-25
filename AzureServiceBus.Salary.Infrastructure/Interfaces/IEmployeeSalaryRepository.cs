using System;
using System.Collections.Generic;
using AzureServiceBus.Salary.Infrastructure.Entities;
using System.Threading.Tasks;

namespace AzureServiceBus.Salary.Infrastructure.Interfaces
{
    public interface IEmployeeSalaryRepository
    {
        Task<EmployeeSalary> GetEmployeeSalaryAsync(string employeeId);
        Task UpdateEmployeeSalaryAsync(EmployeeSalary empSalary);
        Task DeleteEmployeeSalary(EmployeeSalary employeeSalary);
        Task<IList<Employee>> GetEmployees();
        Task<EmployeeSalary> AddEmployeeSalaryAsync(EmployeeSalary empSalary);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Entities.Employee> GetEmployeeByIdAsync(Guid id);
        Task UpdateEmployeeAsync(Employee employee);
    }
}
