using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.AddEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository= userRepository;
        }
        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            //var existingEmployee =Task.WhenAll( _employeeRepository.GetEmployeeByIdAsync(request.Employee.EmailId, cancellationToken));
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.Employee.EmailId, cancellationToken);
            if (existingEmployee != null)
            {
                throw new Exception("Employee with the same email already exists.");
            }

            var employe= await _employeeRepository.CreateEmployeeAsync(request.Employee,cancellationToken);

            var user=_userRepository.GetUserById(employe.EmpId, cancellationToken);
            if(user==null)
            {
                var newUser = new User
                {
                    EmpId = employe.EmpId,
                    DisplayName = employe.FirstName+" " +employe.LastName,
                    Password = employe.FirstName+ employe.ContactNo, // In real scenarios, ensure to hash passwords and not use default ones.
                    Email = employe.EmailId,
                    IsActive = true

                };
                await _userRepository.CreateUser(newUser, cancellationToken);
            }

            return employe;
        }
    }
}
