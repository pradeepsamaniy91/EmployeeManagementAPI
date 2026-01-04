using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployeeByID
{
    public class GetEmployeeByIdQueryHandler:IQueryHandler<GetEmployeeByIdQuery,Employee>
    {

        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Result<Employee?>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Validate the request early
            if (string.IsNullOrWhiteSpace(request.EmailId))
            {
                return Result.Failure<Employee?>("Email address not informed");
            }

            // 2. Correctly await the asynchronous repository call
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmailId, cancellationToken);

            // 3. Return a successful result even if employee is null (consistent with your original logic)
            return Result.Success<Employee?>(employee);
        }

    }

}
