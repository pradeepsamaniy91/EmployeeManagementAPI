using EmployeeManagement.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployeeById
{
    
    public record GetEmployeeByIdQuery(long EmpId) : IQuery<EmployeeResponse?>;
}
