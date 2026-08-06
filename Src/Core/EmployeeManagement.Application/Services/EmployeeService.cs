using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Linq.Expressions;

namespace EmployeeManagement.Application.Services;

public class EmployeeService : IEmployeeRepository
{
    private readonly EmployeeManagementContext _context;
    public EmployeeService(EmployeeManagementContext context)
    {
        _context = context;
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee? employee,CancellationToken cancellationToken)
    {
        var employeeObj = new Employee();
        try
        {
            var result = _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Return the tracked entity (which now includes DB-generated IDs)
            return result.Entity;
        }
        catch (Exception)
        {
            return employeeObj;
        }
    }

    public async Task<Employee?> GetEmployeeByEmailIdAsync(string emailId, CancellationToken cancellationToken)
    {
        Employee employee = null;
        try
        {

            return await _context.Employees
                                 .FirstOrDefaultAsync(e => e.EmailId == emailId, cancellationToken);
                                 
        }
        catch(Exception ex)
        {
            return employee;
        }
    }

    public async Task<Employee?> GetEmployeeByIdAsync(long id, CancellationToken cancellationToken=default)
    {
        return await _context.Employees
                                .FirstOrDefaultAsync(e => e.EmpId == id);
    }

    public async Task<List<Employee>> GetEmployees()
    {
        // Fetches all employees as a list asynchronously
        return await _context.Employees.ToListAsync();
    }
    public async Task<Employee?> RemoveEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        _context.Employees.Update(employee);   // Marks entity for deletion
        await _context.SaveChangesAsync(cancellationToken);  // Await async save
        return employee;
    }
}
