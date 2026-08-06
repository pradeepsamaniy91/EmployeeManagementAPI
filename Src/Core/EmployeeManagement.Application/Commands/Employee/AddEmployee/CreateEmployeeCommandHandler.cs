using EmployeeManagement.Application.Mapper;
using EmployeeManagement.Application.Static;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmployeeManagement.Application.Commands.Employee.AddEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        }
        public async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var usertypeid = StaticUserRole.FromId(request.Employee.UserTypeId);

            if (usertypeid==null)
            {
                return Result.Failure("Invalid User Type.");
            }
            
            //var existingEmployee =Task.WhenAll( _employeeRepository.GetEmployeeByIdAsync(request.Employee.EmailId, cancellationToken));
            var existingEmployee = await _employeeRepository.GetEmployeeByEmailIdAsync(request.Employee.EmailId, cancellationToken);
            if (existingEmployee != null)
            {
               return Result.Failure("Employee with the same email already exists.");
            }
            var employeeEntity = EmployeeMapper.ToEmployee(request.Employee);

            
            var employe= await _employeeRepository.UpdateEmployeeAsync(employeeEntity,cancellationToken);

            User? userExist=await _userRepository.GetUserById(employe.EmpId, cancellationToken);
            if (userExist != null)
            {
                return Result.Failure("User Already exist with the same email already exists.");
            }
            if (userExist == null)
            {
                var user = UserMapper.ToUser(request.Employee,employe.EmpId);
                await _userRepository.CreateUser(user, cancellationToken);
            }
            employe.Password = "*****";
            return Result.Success(employe);
        }
    }
}
