using EmployeeManagement.Application.Commands.Employee.AddEmployee;
using EmployeeManagement.Application.Queries.GetEmployeeById;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.Employee.Remove
{
    internal class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;
       
        public RemoveEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
           
        }

        public object? logger { get; private set; }

        public async Task<Result> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId, cancellationToken);

            if (existingEmployee != null) {

                existingEmployee.IsActive = "0";
                existingEmployee.UpdatedOn = DateTime.Now;
                _employeeRepository.RemoveEmployeeAsync(existingEmployee, cancellationToken);
            }

            return Result.Success(request);
        }
    }
}
