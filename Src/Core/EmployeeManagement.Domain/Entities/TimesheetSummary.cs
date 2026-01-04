using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class TimesheetSummary
{
    public long SummaryId { get; set; }

    public int PeriodId { get; set; }

    public long UserId { get; set; }

    public int TotalMinutes { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? SubmittedAt { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public long? ApprovedByUserId { get; set; }

    public virtual User? ApprovedByUser { get; set; }

    public virtual TimesheetPeriod Period { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
