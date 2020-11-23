using FluentValidation;
using AzureServiceBus.Employee.Infrastructure.Entities;

namespace AzureServiceBus.Employee.API.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee.Infrastructure.Entities.Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty();
            RuleFor(x => x.EmpCode).NotNull().NotEmpty();
        }
    }
}
