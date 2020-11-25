using AzureServiceBus.Salary.API.Core.IntegrationEvents.Events;
using AzureServiceBusLibrary.EventBus.Events.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AzureServiceBus.Salary.Infrastructure.Entities;
using AzureServiceBus.Salary.Infrastructure.Interfaces;

namespace AzureServiceBus.Salary.API.Core.IntegrationEvents.EventHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeChangedIntegrationEventHandler : IIntegrationEventHandler<EmployeeChangedIntegrationEvent>
    {
        private readonly ILogger<EmployeeChangedIntegrationEventHandler> _logger;
        private readonly IEmployeeSalaryRepository _empSalaryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empSalaryRepository"></param>
        public EmployeeChangedIntegrationEventHandler(ILogger<EmployeeChangedIntegrationEventHandler> logger,
                                                            IEmployeeSalaryRepository empSalaryRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _empSalaryRepository = empSalaryRepository ?? throw new ArgumentNullException(nameof(empSalaryRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(EmployeeChangedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            var employeeToUpdate = await _empSalaryRepository.GetEmployeeByIdAsync(@event.EmployeeId);
            if (employeeToUpdate != null)
            {
                // Update Employee
                employeeToUpdate.FirstName = @event.FirstName;
                employeeToUpdate.LastName = @event.LastName;

                await _empSalaryRepository.UpdateEmployeeAsync(employeeToUpdate);
                var existingSalary = await _empSalaryRepository.GetEmployeeSalaryAsync(@event.EmployeeId.ToString());
                if (existingSalary != null)
                {
                    // UpdateExisting Salary
                    existingSalary.StartDate = @event.StartDate;
                    existingSalary.EndDate = @event.EndDate;
                    existingSalary.Salary = @event.NewSalary;
                    existingSalary.ModifiedOn = DateTime.Now;
                    existingSalary.ModifiedBy = @event.ModifiedBy;

                    await _empSalaryRepository.UpdateEmployeeSalaryAsync(existingSalary);
                }
                else
                {
                    await _empSalaryRepository.AddEmployeeSalaryAsync(new EmployeeSalary()
                    {
                        EmployeeId = @event.EmployeeId,
                        Salary = @event.NewSalary,
                        StartDate = @event.StartDate,
                        EndDate = @event.EndDate,
                        CreatedBy = @event.ModifiedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = @event.ModifiedBy,
                        ModifiedOn = DateTime.Now
                    });
                }
            }
        }
    }
}
