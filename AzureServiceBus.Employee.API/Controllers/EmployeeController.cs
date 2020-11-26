using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureServiceBus.Employee.API.Core.IntegrationEvents.Events;
using AzureServiceBus.Employee.Infrastructure.Data;
using AzureServiceBus.Employee.Infrastructure.Interfaces;
using AzureServiceBus.Employee.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzureServiceBus.Employee.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeService;
        private readonly IEmployeeIntegrationEventService _employeeIntegrationEventService;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="employeeIntegrationEventService"></param>
        /// <param name="logger"></param>
        public EmployeeController(IEmployeeRepository employeeService,
            IEmployeeIntegrationEventService employeeIntegrationEventService, ILogger logger)
        {
            _employeeService = employeeService;
            _employeeIntegrationEventService = employeeIntegrationEventService;
            _logger = logger;
        }

        /// <summary>
        /// Gets list with available employees in the catalog
        /// </summary>
        [ProducesResponseType(typeof(IList<Infrastructure.Entities.Employee>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _employeeService.ListAllAsync();
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
            var employee = await _employeeService.GetByIdAsync(id);
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
            var userIdentity = Request.Headers["claims_userid"];
            if (userIdentity.Count == 0)
                return NotFound(new { Message = $"You are not authorized to add employee." });

            employee.CreatedBy = employee.ModifiedBy = userIdentity;

            try
            {
                // Commit Add Employee
                var addedEmployee = await _employeeService.AddEmployee(employee);

                var eployeeChangedEvent = new EmployeeAddIntegrationEvent(employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.ModifiedBy);

                await _employeeIntegrationEventService.AddAndSaveEventAsync(eployeeChangedEvent);
                await _employeeIntegrationEventService.PublishEventsThroughEventBusAsync(eployeeChangedEvent);

                return Ok(employee.Id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Update existing employee in the catalog
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] Infrastructure.Entities.Employee employeeToUpdate)
        {
            var userIdentity = Request.Headers["claims_userid"];
            if (userIdentity.Count == 0)
                return NotFound(new { Message = $"You are not authorized to add employee." });

            employeeToUpdate.ModifiedBy = userIdentity;
            var existingEmployee = await _employeeService.GetByIdAsync(employeeToUpdate.Id);
            if (existingEmployee == null)
            {
                return NotFound(new { Message = $"Employee with id {employeeToUpdate.Id} not found." });
            }
            else
            {
                // Update Employee
                existingEmployee.FirstName = employeeToUpdate.FirstName;
                existingEmployee.LastName = employeeToUpdate.LastName;
                existingEmployee.Address = employeeToUpdate.Address;
                existingEmployee.Contact = employeeToUpdate.Contact;
                existingEmployee.DepartmentName = employeeToUpdate.DepartmentName;
                existingEmployee.EmpCode = employeeToUpdate.EmpCode;
                existingEmployee.IsActive = employeeToUpdate.IsActive;
                existingEmployee.JoiningDate = employeeToUpdate.JoiningDate;
                existingEmployee.ModifiedBy = employeeToUpdate.ModifiedBy;

                // Subscribe Event
                var eployeeChangedEvent = new EmployeeChangedIntegrationEvent(employeeId: existingEmployee.Id,
                    firstName: employeeToUpdate.FirstName,
                    lastName: employeeToUpdate.LastName,
                    address: employeeToUpdate.Address,
                    contact: employeeToUpdate.Contact,
                    departmentName: employeeToUpdate.DepartmentName,
                    joiningDate: employeeToUpdate.JoiningDate,
                    empCode: employeeToUpdate.EmpCode,
                    isActive: employeeToUpdate.IsActive,
                    modifiedBy: employeeToUpdate.ModifiedBy,
                    // Default Values For Salary
                    salary: 2000,
                    startDate: DateTime.Now,
                    endDate: DateTime.Now.AddDays(90));

                await _employeeIntegrationEventService.AddAndSaveEventAsync(eployeeChangedEvent);
                await _employeeIntegrationEventService.PublishEventsThroughEventBusAsync(eployeeChangedEvent);

                // Commit Update Employee
                await _employeeService.UpdateEmployee(existingEmployee);
                return NoContent();
            }
        }
    }
}
