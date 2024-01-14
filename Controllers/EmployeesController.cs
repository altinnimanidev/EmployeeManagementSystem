using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Controller]
    [Route("Employees")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                // Handle invalid ID (e.g., return a not found view or redirect)
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                // Handle the case where the employee doesn't exist
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                // Handle the case where the model is not valid
                return BadRequest(ModelState);
            }

            try
            {
                Employee? newEmployee = new Employee();
                if (employee.Id == 0)
                {
                    newEmployee = await _employeeService.AddEmployeeAsync(employee);
                }
                else
                {
                    newEmployee = await _employeeService.UpdateEmployeeAsync(employee);
                }

                if (newEmployee == null)
                {
                    return NotFound();
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the exception and handle the error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating employee");
            }
        }

        [HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [Route("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployeeAsync(id);
         
            return RedirectToAction("Index", "Home");
        }
    }
}
