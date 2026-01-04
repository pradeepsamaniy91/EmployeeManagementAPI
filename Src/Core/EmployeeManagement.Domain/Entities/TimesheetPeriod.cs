using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class TimesheetPeriod
{
    public int PeriodId { get; set; }

    public string PeriodType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<TimesheetSummary> TimesheetSummaries { get; set; } = new List<TimesheetSummary>();
}
