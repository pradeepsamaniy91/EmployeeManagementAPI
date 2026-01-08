using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Mapper
{
    public static class UserMapper
    {
        public static User ToUser(EmployeeDto employeeDto,long EmpID)
        {
            return new User
            {
                // Assuming EmpId is generated elsewhere and not part of EmployeeDto
                EmpId=EmpID,
                DisplayName = $"{employeeDto.FirstName} {employeeDto.LastName}",               
                Email = employeeDto.EmailId,
                IsActive = true,
                UserTypeId = employeeDto.UserTypeId
            };
        }
    }
}
