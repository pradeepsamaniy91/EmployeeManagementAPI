using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee?>> GetEmployees();
    Task<Employee?> GetEmployeeByIdAsync(long id,CancellationToken cancellationToken);
    Task<Employee?> GetEmployeeByEmailIdAsync(string emailId,CancellationToken cancellationToken);
    Task<Employee?> CreateEmployeeAsync(Employee employee);
    Task<Employee?> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);


    
}
