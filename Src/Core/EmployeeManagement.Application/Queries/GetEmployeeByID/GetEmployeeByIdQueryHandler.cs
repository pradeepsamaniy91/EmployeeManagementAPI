using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployeeByID
{
    public class GetEmployeeByIdQueryHandler:IQueryHandler<GetEmployeeByIdQuery,Employee>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeeByIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Result<Employee?>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {MethodName} for EmailId: {EmailId}", nameof(Handle), request.EmailId);

            // 1. Validate the request early
            if (string.IsNullOrWhiteSpace(request.EmailId))
            {
                return Result.Failure<Employee?>("Email address not informed");
            }

            // 2. Correctly await the asynchronous repository call
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmailId, cancellationToken);
            _logger.LogInformation("response from database", nameof(Handle), employee);

            // 3. Return a successful result even if employee is null (consistent with your original logic)
            return Result.Success<Employee?>(employee);
        }

    }

}
