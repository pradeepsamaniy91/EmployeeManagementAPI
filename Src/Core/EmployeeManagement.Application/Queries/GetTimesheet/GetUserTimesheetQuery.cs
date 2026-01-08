using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries.GetTimesheet;

public record GetUserTimesheetQuery(int Month,int Year) : IQuery<IEnumerable<object>>;
