using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagementInterface.API.Mapper
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
                Status = dto.Status,
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
