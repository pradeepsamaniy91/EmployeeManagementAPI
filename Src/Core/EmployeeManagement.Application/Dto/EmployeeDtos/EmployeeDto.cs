using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Dto.EmployeeDtos;


public class EmployeeDto
{
    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string EmailId { get; set; } = null!;

    public string? Gender { get; set; }

    public bool? Status { get; set; }

    public long ContactNo { get; set; }

    public string? EmployeeType { get; set; }

    public int BillRate { get; set; }

    public int PayRate { get; set; }

    public string? Client { get; set; }

    public string? Reference { get; set; }
    public int  UserTypeId { get; set; }

    public DateOnly HireDate { get; set; }

   
}