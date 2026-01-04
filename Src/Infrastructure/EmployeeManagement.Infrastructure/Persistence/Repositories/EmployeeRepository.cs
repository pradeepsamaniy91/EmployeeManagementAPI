using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly ILogger<> _logger;
        public EmployeeRepository(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository?? throw new ArgumentNullException(nameof(employeeRepository));
        }
        Task<Employee> IEmployeeRepository.CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Employee> IEmployeeRepository.GetEmployeeByIdAsync(string emailId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Employee> IEmployeeRepository.GetEmployees()
        {
            throw new NotImplementedException();
        }

        Task<Employee> IEmployeeRepository.UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
