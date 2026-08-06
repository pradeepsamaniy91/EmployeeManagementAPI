using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployeeByID
{
    public record GetEmployeeByEmailIdQuery(string EmailId):IQuery<EmployeeResponse?>;
    
}
