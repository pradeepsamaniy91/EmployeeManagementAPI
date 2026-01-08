using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<Result> CreateEmployeeAsync(Employee employee);
        Task<Employee?> GetEmployeeByIdAsync(long empId);
        Task<Employee?> GetEmployeeByEmailIdAsync(string emailId);
    }
}
