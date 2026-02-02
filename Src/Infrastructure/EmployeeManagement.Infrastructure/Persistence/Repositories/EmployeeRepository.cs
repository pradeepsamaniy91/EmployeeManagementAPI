using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmployeeManagement.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementContext _context;
        //private readonly ILogger<> _logger;
        public EmployeeRepository(EmployeeManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee?> GetAsync(long empId, CancellationToken cancellationToken=default)
        {
            // Use .AsNoTracking() for read-only queries to improve performance.
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmpId == empId, cancellationToken);
        }
        public async Task<Employee?> GetEmployeeByIdAsync(string emailId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.EmailId == emailId, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // 2026 Best Practice: Task was canceled by the caller/user.
                // Return null or rethrow based on your business logic.
                return null;
            }
            catch (Exception ex)
            {
                // Log actual database/logic errors here
                throw;
            }
        }

        public async Task<List<Employee?>> GetEmployees()
        {
            return await _context?.Employees?.ToListAsync();

        }

        Task<Employee> IEmployeeRepository.UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken=default)
        {
            throw new NotImplementedException();
        }
    }
}
