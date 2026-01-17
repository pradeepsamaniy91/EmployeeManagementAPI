using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class RefreshToken
{
    public int Id { get; set; }

    public long? EmpId { get; set; }

    public string? RefreshToken1 { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual Employee? Emp { get; set; }
}
