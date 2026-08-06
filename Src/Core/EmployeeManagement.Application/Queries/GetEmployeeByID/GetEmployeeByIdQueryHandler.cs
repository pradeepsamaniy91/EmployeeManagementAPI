using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Application.Mapper;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployeeById
{

    public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, EmployeeResponse>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeeByIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<EmployeeResponse?>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {MethodName} for EmpID: {EmpID}", nameof(Handle), request.EmpId);

            // 1. Validate the request early
            if (request?.EmpId == null)
            {
                return Result.Failure<EmployeeResponse?>("Emp Id is required");
            }

            // 2. Correctly await the asynchronous repository call
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmpId, cancellationToken);
            _logger.LogInformation("response from database", nameof(Handle), employee);
            var employeeResult = EmployeeMapper.ToEmployeeResponse(employee);

            // 3. Return a successful result even if employee is null (consistent with your original logic)
            return Result.Success<EmployeeResponse?>(employeeResult);
        }
    }
}
