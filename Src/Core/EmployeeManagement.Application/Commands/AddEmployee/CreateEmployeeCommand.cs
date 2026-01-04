using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EmployeeManagement.Application.Commands.AddEmployee
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        // Ensure the property name matches what you are assigning in the constructor
        public CreateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
        public Employee Employee { get; set; }
    }
}
