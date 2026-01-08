using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployees();
    Task<Employee?> GetAsync(long id,CancellationToken cancellationToken);
    Task<Employee?> GetEmployeeByIdAsync(string emailId,CancellationToken cancellationToken);
    Task<Employee?> CreateEmployeeAsync(Employee employee);
    Task<Employee?> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);


    
}
