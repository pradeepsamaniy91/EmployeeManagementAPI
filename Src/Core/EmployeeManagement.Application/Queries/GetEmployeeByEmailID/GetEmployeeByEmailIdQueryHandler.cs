using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Application.Mapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployeeByID
{
    public class GetEmployeeByEmailIdQueryHandler:IQueryHandler<GetEmployeeByEmailIdQuery, EmployeeResponse>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeeByEmailIdQueryHandler> _logger;
        public GetEmployeeByEmailIdQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeeByEmailIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Result<EmployeeResponse?>> Handle(GetEmployeeByEmailIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {MethodName} for EmailId: {EmailId}", nameof(Handle), request.EmailId);

            // 1. Validate the request early
            if (string.IsNullOrWhiteSpace(request.EmailId))
            {
                return Result.Failure<EmployeeResponse?>("Email address not informed");
            }

            // 2. Correctly await the asynchronous repository call
            var employee = await _employeeRepository.GetEmployeeByEmailIdAsync(request.EmailId, cancellationToken);
            _logger.LogInformation("response from database", nameof(Handle), employee);
            var employeeResult=EmployeeMapper.ToEmployeeResponse(employee);

            // 3. Return a successful result even if employee is null (consistent with your original logic)
            return Result.Success<EmployeeResponse?>(employeeResult);
        }

    }

}
