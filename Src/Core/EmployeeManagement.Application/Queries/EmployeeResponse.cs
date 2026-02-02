using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Queries
{
    public class EmployeeResponse
    {
        public long EmpId { get; set; }
        public long ManagerId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string? EmployeeType { get; set; }
        public decimal BillRate { get; set; }
        public decimal PayRate { get; set; }
        public string? Client { get; set; }
        public string? Reference { get; set; }
        public string? IsActive { get; set; }
        public DateOnly? HireDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }


}
