using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureServiceBus.Salary.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EmployeeSalaryController : ControllerBase
    {
        private readonly ILogger<EmployeeSalaryController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EmployeeSalaryController(ILogger<EmployeeSalaryController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Employee Salary Micro Service Is Up And Running!");
        }
    }
}
