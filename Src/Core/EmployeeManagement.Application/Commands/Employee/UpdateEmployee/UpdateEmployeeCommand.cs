using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.Employee.UpdateEmployee
{
    
    public record UpdateEmployeeCommand : IRequest<Result>
    {
        public UpdateEmployeeCommand(EmployeeDto employee)
        {
            Employee = employee;
        }
        public EmployeeDto Employee { get; set; }
    }
}
