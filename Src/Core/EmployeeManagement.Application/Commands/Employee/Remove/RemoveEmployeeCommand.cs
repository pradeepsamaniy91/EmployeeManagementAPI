using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.Employee.Remove
{
    public class RemoveEmployeeCommand: IRequest<Result>
    {
        public int EmployeeId { get; set; }
        public RemoveEmployeeCommand(int employeeId)
        {
                EmployeeId = employeeId;
        }
    }
}
