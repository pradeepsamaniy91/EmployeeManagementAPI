using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities;

public partial class Employee
{
    public long EmpId { get; set; }

    public long? ManagerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string EmailId { get; set; } = null!;

    public string? Gender { get; set; }

    public bool? Status { get; set; }

    public long ContactNo { get; set; }

    public string? EmployeeType { get; set; }

    public string? Password { get; set; }

    public int BillRate { get; set; }

    public int PayRate { get; set; }

    public string? Client { get; set; }

    public string? Reference { get; set; }

    public string? IsActive { get; set; }

    public DateOnly HireDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? LastUpdatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
