﻿using AzureServiceBus.Salary.API.Core.IntegrationEvents.Events;
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
    public class EmployeeAddedIntegrationEventHandler : IIntegrationEventHandler<EmployeeAddIntegrationEvent>
    {
        private readonly ILogger<EmployeeAddedIntegrationEventHandler> _logger;
        private readonly IEmployeeSalaryRepository _empSalaryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empSalaryRepository"></param>
        public EmployeeAddedIntegrationEventHandler(ILogger<EmployeeAddedIntegrationEventHandler> logger,
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
        public async Task HandleAsync(EmployeeAddIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            var newEmployee = new Employee()
            {
                EmployeeId = @event.EmployeeId,
                FirstName = @event.FirstName,
                LastName = @event.LastName
            };
            newEmployee = await _empSalaryRepository.AddEmployeeAsync(newEmployee);
            await AddSalaryForEmployee(@event.EmployeeId, @event.Salary, @event.StartDate, @event.EndDate, @event.ModifiedBy);
        }

        private async Task AddSalaryForEmployee(Guid employeeId, decimal? salary, DateTime? startDate, DateTime? endDate, string modifiedBy = null)
        {
            await _empSalaryRepository.AddEmployeeSalaryAsync(new EmployeeSalary()
            {
                EmployeeId = employeeId,
                Salary = salary ?? decimal.Zero,
                StartDate = startDate,
                EndDate = endDate,
                CreatedBy = modifiedBy,
                CreatedOn = DateTime.Now,
                ModifiedBy = modifiedBy,
                ModifiedOn = DateTime.Now
            });
        }
    }
}
