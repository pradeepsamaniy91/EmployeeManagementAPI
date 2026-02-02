using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployeeByID
{
    public record GetEmployeeByIdQuery(string EmailId):IQuery<EmployeeResponse?>;
    
}
