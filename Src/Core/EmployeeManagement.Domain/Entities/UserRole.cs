using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class UserRole
{
    public int Id { get; set; }

    public string? UserType { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    
}
