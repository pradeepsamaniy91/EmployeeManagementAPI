using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class MonthlyAttendanceSummary
{
    public int? EmpId { get; set; }

    public int? PresentCount { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }
}
