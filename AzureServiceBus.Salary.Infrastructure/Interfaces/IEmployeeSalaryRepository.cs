using AzureServiceBus.Salary.Infrastructure.Entities;
using System.Threading.Tasks;

namespace AzureServiceBus.Salary.Infrastructure.Interfaces
{
    public interface IEmployeeSalaryRepository
    {
        Task<EmployeeSalary> GetEmployeeSalaryAsync(string employeeId);
        Task<EmployeeSalary> UpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary);
        void DeleteEmployeeSalary(EmployeeSalary employeeSalary);
    }
}
