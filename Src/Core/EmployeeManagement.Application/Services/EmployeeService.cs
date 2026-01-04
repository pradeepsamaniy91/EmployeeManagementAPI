using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Linq.Expressions;

namespace EmployeeManagement.Application.Services;

public class EmployeeService : IEmployeeRepository, IUserRepository
{
    private readonly EmployeeManagementContext _context;
    public EmployeeService(EmployeeManagementContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateEmployeeAsync(Employee? employee)
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

    Task<User?> IUserRepository.CreateUser(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(string emailId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(emailId))
            throw new ArgumentException("emailId must be provided", nameof(emailId));

        // Respect the caller's CancellationToken and do not swallow cancellation exceptions.
        // Let EF Core observe the token so the DB call can be cancelled by the caller (e.g., HttpContext.RequestAborted).
        return await _context.Employees
                             .FirstOrDefaultAsync(e => e.EmailId == emailId, cancellationToken)
                             .ConfigureAwait(false);
    }

    Task<Employee> IEmployeeRepository.GetEmployees()
    {
        throw new NotImplementedException();
    }

    Task<User?> IUserRepository.GetUserAsync()
    {
        throw new NotImplementedException();
    }

    async Task<User?> IUserRepository.GetUserById(long id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.EmpId == id, cancellationToken);
    }

    Task<List<User?>> IUserRepository.GetUsersByIDAndPasswordAsync(string userName, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<Employee> IEmployeeRepository.UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<Employee?> IEmployeeRepository.CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
