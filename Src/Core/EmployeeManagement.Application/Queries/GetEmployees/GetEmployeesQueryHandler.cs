using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Application.Mapper;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetEmployees
{
    // 1. Ensure the record matches the expected return type (removed '?' for cleaner list handling)
    

    public class GetEmployeesQueryHandler : IQueryHandler<GetEmployeesQuery, List<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeesQueryHandler> _logger;

        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeesQueryHandler> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // 2. Added missing parameters: 'GetEmployeesQuery request' and 'CancellationToken cancellationToken'
        public async Task<Result<List<EmployeeResponse>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {MethodName}", nameof(Handle));

            // 3. Fetch data from repository
            var employees = await _employeeRepository.GetEmployees();

            _logger.LogInformation("Successfully retrieved {Count} employees from database.", employees?.Count ?? 0);

            // 4. Map the list using LINQ Select (Ensure EmployeeMapper.ToEmployeeResponse exists)
            //var employeeResult = employees?
            //    .Select(e => EmployeeMapper.ToEmployeeResponse(e))
            //    .ToList() ?? new List<EmployeeResponse>();
            var employeeResult = EmployeeMapper.ToEmployeeList(employees!);

            // 5. Return success
            return Result.Success(employeeResult);
        }
    }


}
