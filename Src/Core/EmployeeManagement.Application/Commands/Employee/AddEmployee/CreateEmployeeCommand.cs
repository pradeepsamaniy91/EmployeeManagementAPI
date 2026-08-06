using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Commands.Employee.AddEmployee;

public class CreateEmployeeCommand : IRequest<Result>
{
    // Ensure the property name matches what you are assigning in the constructor
    public CreateEmployeeCommand(EmployeeDto employee)
    {
        Employee = employee;
    }
    public EmployeeDto Employee { get; set; }
}
