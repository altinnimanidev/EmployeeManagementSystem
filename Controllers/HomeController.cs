using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

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
        public async Task<ActionResult<IPagedList<Employee>>> Index(int page = 1, int pageSize = 10) // Default is page 1 and 10 items per page
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                if (employees == null)
                {
                    return NotFound();
                }

                var pagedEmployees = employees.ToPagedList(page, pageSize); // Using X.PagedList
                return View(pagedEmployees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database\n" + ex.Message);
            }
        }

    }
}
