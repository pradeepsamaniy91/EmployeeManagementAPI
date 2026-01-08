using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class User
{
    public long UserId { get; set; }

    public long EmpId { get; set; }

    public string DisplayName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? UserTypeId { get; set; }

    public virtual UserRole? UserType { get; set; }

    public virtual ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
}
