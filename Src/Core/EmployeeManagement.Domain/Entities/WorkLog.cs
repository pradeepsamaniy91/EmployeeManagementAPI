using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class WorkLog
{
    public long WorkLogId { get; set; }

    public long UserId { get; set; }

    public DateOnly WorkDate { get; set; }

    public decimal HoursWorked { get; set; }

    public string? Description { get; set; }

    public long? CreatedByUserId { get; set; }

    public long? UpdatedByUserId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual User User { get; set; } = null!;
}
