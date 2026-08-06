using EmployeeManagement.Application.Commands.Employee.AddEmployee;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.Employee.Remove
{
    internal class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        public async Task<Result> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId, cancellationToken);

            if (existingEmployee == null) { 

            }

            return Result.Success(request);
        }
    }
}
