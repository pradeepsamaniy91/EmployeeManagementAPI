using EmployeeManagement.Application.Commands.Employee.AddEmployee;
using EmployeeManagement.Application.Mapper;
using EmployeeManagement.Application.Static;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Commands.Employee.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    }
    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        

        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.Employee.Id, cancellationToken);

        if (existingEmployee == null)
        {
            return Result.Failure("Employee does not exists.");
        }

        var employeeEntity = EmployeeMapper.ToUpdateEmployee(request.Employee, existingEmployee);

        var employee = await _employeeRepository.UpdateEmployeeAsync(employeeEntity,cancellationToken);


        return Result.Success(employee);
    }
}