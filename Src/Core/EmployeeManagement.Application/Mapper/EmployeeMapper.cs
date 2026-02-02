using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Application.Queries;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Mapper
{
    public static class EmployeeMapper
    {
        public static Employee ToEmployee(EmployeeDto dto)
        {
            return new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailId = dto.EmailId,
                Gender = dto.Gender,
                Password = $"{dto.FirstName}{dto.ContactNo}", // In real scenarios, ensure to hash passwords and not use default ones.
                ContactNo = dto.ContactNo,
                EmployeeType = dto.EmployeeType,
                BillRate = dto.BillRate,
                PayRate = dto.PayRate,
                Client = dto.Client,
                Reference = dto.Reference,
                HireDate = dto.HireDate,
                CreatedOn = DateTime.Now
            };
        }
        public static EmployeeResponse ToEmployeeResponse(Employee employee)
        {
            return new EmployeeResponse
            {
                EmpId = employee.EmpId,
                ManagerId = employee.ManagerId ?? 0,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmailId = employee.EmailId,
                Gender= employee.Gender,
                ContactNo  =employee.ContactNo,
                EmployeeType=employee.EmployeeType,
                BillRate = employee.BillRate,
                PayRate=employee.PayRate,
                Client=employee.Client,
                Reference = employee.Reference,
                IsActive =employee.IsActive,
                HireDate=employee.HireDate,
                EndDate=employee.EndDate,
                CreatedBy=employee.CreatedBy,
                LastUpdatedBy=employee.LastUpdatedBy,
                CreatedOn=employee.CreatedOn,
                UpdatedOn=employee.UpdatedOn

            };
        }
        public static List<EmployeeResponse> ToEmployeeList(List<Employee> employees)
        {
            return employees.Select(e => new EmployeeResponse
            {
                EmpId = e.EmpId,
                ManagerId = e.ManagerId ?? 0,
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmailId = e.EmailId,
                Gender = e.Gender,
                ContactNo = e.ContactNo,
                EmployeeType = e.EmployeeType,
                BillRate = e.BillRate,
                PayRate = e.PayRate,
                Client = e.Client,
                Reference = e.Reference,
                IsActive = e.IsActive,
                HireDate = e.HireDate,
                EndDate = e.EndDate,
                CreatedBy = e.CreatedBy,
                LastUpdatedBy = e.LastUpdatedBy,
                CreatedOn = e.CreatedOn,
                UpdatedOn = e.UpdatedOn

            }).ToList();
        }
    }

}
