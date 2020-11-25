using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureServiceBus.Employee.API.Core.IntegrationEvents.Events;
using AzureServiceBus.Employee.Infrastructure.Data;
using AzureServiceBus.Employee.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzureServiceBus.Employee.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly IEmployeeIntegrationEventService _employeeIntegrationEventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeDbContext"></param>
        /// <param name="employeeIntegrationEventService"></param>
        public EmployeeController(EmployeeDbContext employeeDbContext,
            IEmployeeIntegrationEventService employeeIntegrationEventService)
        {
            _employeeDbContext = employeeDbContext;
            _employeeIntegrationEventService = employeeIntegrationEventService;
        }

        /// <summary>
        /// Gets list with available employees in the catalog
        /// </summary>
        [ProducesResponseType(typeof(IList<Infrastructure.Entities.Employee>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        /// <summary>
        /// Gets employee from the catalog
        /// </summary>
        [ProducesResponseType(typeof(Infrastructure.Entities.Employee), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid id)
        {
            var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(i => i.Id == id);
            if (employee == null)
            {
                return NotFound(new { Message = $"Employee with id {id} not found." });
            }
            return Ok(employee);
        }

        /// <summary>
        /// Add new employee to the catalog
        /// </summary>
        [ProducesResponseType(typeof(Infrastructure.Entities.Employee), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] Infrastructure.Entities.Employee employee)
        {
            var addedEmployee = _employeeDbContext.Add(employee);
            var eployeeChangedEvent = new EmployeeAddIntegrationEvent(employee.Id,
                employee.FirstName,
                employee.LastName);

            // await _employeeIntegrationEventService.AddAndSaveEventAsync(eployeeChangedEvent);
            await _employeeIntegrationEventService.PublishEventsThroughEventBusAsync(eployeeChangedEvent);
            await _employeeDbContext.SaveChangesAsync();
            return Ok(employee.Id);
        }


        /// <summary>
        /// Update existing employee in the catalog
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] Infrastructure.Entities.Employee employeeToUpdate)
        {
            var existingEmployee = await _employeeDbContext.Employees.FirstOrDefaultAsync(i => i.Id == employeeToUpdate.Id);
            if (existingEmployee == null)
            {
                return NotFound(new { Message = $"Employee with id {employeeToUpdate.Id} not found." });
            }
            else
            {
                _employeeDbContext.Employees.Update(existingEmployee);
                var eployeeChangedEvent = new EmployeeChangedIntegrationEvent(existingEmployee.Id,
                    employeeToUpdate.FirstName,
                    employeeToUpdate.LastName,
                    employeeToUpdate.Address,
                    employeeToUpdate.Contact,
                    employeeToUpdate.DepartmentName,
                    employeeToUpdate.JoiningDate,
                    employeeToUpdate.EmpCode,
                    employeeToUpdate.IsActive);

               // await _employeeIntegrationEventService.AddAndSaveEventAsync(eployeeChangedEvent);
                await _employeeIntegrationEventService.PublishEventsThroughEventBusAsync(eployeeChangedEvent);

                await _employeeDbContext.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
