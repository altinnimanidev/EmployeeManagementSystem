using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Employee>>> Index()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                if (employees == null)
                {
                    return NotFound();
                }

                return View(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database\n" + ex.Message);
            }
        }
    }
}
