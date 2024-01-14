using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                // Use ToListAsync() to asynchronously retrieve data from the database
                var employees = await _context.Employees.ToListAsync();

                // Return the retrieved data
                return employees;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id) ?? null;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (existingEmployee == null)
            {
                // Employee not found
                return null;
            }

            // Update properties
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Position = employee.Position;
            existingEmployee.Salary = employee.Salary;

            try
            {
                _context.Update(existingEmployee);
                await _context.SaveChangesAsync();
                return existingEmployee;
            }
            catch
            {
                throw; // handle the exception
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
