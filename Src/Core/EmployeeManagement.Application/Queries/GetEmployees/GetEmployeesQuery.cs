using EmployeeManagement.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployees
{
    public record GetEmployeesQuery : IQuery<List<EmployeeResponse?>>;
}
