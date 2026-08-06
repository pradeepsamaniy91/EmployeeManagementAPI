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
       
       
        public async Task<List<Employee?>> GetEmployees()
        {
            return await _context?.Employees?.ToListAsync();

        }

        async Task<Employee?> IEmployeeRepository.GetEmployeeByEmailIdAsync(string emailId, CancellationToken cancellationToken)
        {
          
                return await _context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.EmailId == emailId, cancellationToken);
            
        }

       async Task<Employee?> IEmployeeRepository.GetEmployeeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.EmpId == id, cancellationToken);
        }

        Task<Employee> IEmployeeRepository.UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken=default)
        {
            throw new NotImplementedException();
        }
    }
}
