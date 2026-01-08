using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetTimesheet;

// Ensure the interface matches: IQueryHandler<RequestType, ResponseType>
public class GetUserTimesheetQueryHandler : IQueryHandler<GetUserTimesheetQuery, IEnumerable<object>>
{
    private readonly ITimeSheet _times;

    public GetUserTimesheetQueryHandler(ITimeSheet times)
    {
        _times = times ?? throw new ArgumentNullException(nameof(times));
    }

    public async Task<Result<IEnumerable<object>>> Handle(GetUserTimesheetQuery request, CancellationToken cancellationToken)
    {
        // Fetch data from service (returns dynamic rows from your SQL Pivot)
        var timesheet = await _times.GetTimesheetAsync(request.Month, request.Year, cancellationToken);

        if (timesheet == null)
        {
            return Result.Failure<IEnumerable<object>>("No data found.");
        }

        // Return the actual data retrieved from the database
        return Result.Success(timesheet);
    }
}
