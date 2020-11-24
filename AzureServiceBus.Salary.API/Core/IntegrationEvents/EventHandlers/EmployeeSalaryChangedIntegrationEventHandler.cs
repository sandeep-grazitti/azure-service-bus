using AzureServiceBus.Salary.API.Core.IntegrationEvents.Events;
using AzureServiceBusLibrary.EventBus.Events.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AzureServiceBus.Salary.Infrastructure.Entities;
using AzureServiceBus.Salary.Infrastructure.Interfaces;

namespace AzureServiceBus.Salary.API.Core.IntegrationEvents.EventHandlers
{
    public class EmployeeSalaryChangedIntegrationEventHandler : IIntegrationEventHandler<EmployeeChangedIntegrationEvent>
    {
        private readonly ILogger<EmployeeSalaryChangedIntegrationEventHandler> _logger;
        private readonly IEmployeeSalaryRepository _empSalaryRepository;

        public EmployeeSalaryChangedIntegrationEventHandler(ILogger<EmployeeSalaryChangedIntegrationEventHandler> logger,
                                                            IEmployeeSalaryRepository empSalaryRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _empSalaryRepository = empSalaryRepository ?? throw new ArgumentNullException(nameof(empSalaryRepository));
        }

        public async Task HandleAsync(EmployeeChangedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            var employeeSalary = await _empSalaryRepository.GetEmployeeSalaryAsync(@event.EmployeeId.ToString());
            await UpdateSalaryForEmployee(@event.EmployeeId, @event.NewSalary, @event.OldSalary, @event.StartDate, @event.EndDate, employeeSalary);
        }

        private async Task UpdateSalaryForEmployee(Guid employeeId, decimal newSalary, decimal oldSalary, DateTime? startDate, DateTime? endDate,
            EmployeeSalary empSalary)
        {
            if (empSalary != null)
            {
                _logger.LogInformation(
                    $"{nameof(EmployeeSalaryChangedIntegrationEventHandler)} - Updating employee salary: {empSalary.EmployeeId}",
                    empSalary.EmployeeId);

                if (empSalary.Salary == oldSalary)
                {
                    empSalary.Salary = newSalary;
                }

                await _empSalaryRepository.UpdateEmployeeSalaryAsync(empSalary);
            }
            else
            {
                await _empSalaryRepository.AddEmployeeSalaryAsync(new EmployeeSalary()
                {
                    Salary = newSalary,
                    StartDate = startDate,
                    EndDate = endDate
                });
            }
        }
    }
}
