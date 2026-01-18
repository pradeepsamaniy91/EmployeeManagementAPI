using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Mapper
{
    public static class EmployeeMapper
    {
        public static Employee ToEmployee(EmployeeDto dto)
        {
            return new Employee { 
                FirstName = dto.FirstName,
                LastName= dto.LastName,
                EmailId = dto.EmailId,
                Gender = dto.Gender,             
                Password = $"{dto.FirstName}{dto.ContactNo}", // In real scenarios, ensure to hash passwords and not use default ones.
                ContactNo = dto.ContactNo,
                EmployeeType = dto.EmployeeType,
                BillRate = dto.BillRate,
                PayRate = dto.PayRate,
                Client=dto.Client,
                Reference=dto.Reference,
                HireDate= dto.HireDate,
                CreatedOn=DateTime.Now
            };
        }
    }
}
