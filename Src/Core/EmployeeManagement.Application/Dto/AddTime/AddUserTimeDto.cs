using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Dto.AddTime
{
    public record AddUserTimeDto(long userId,DateOnly Date,decimal HoursWorked, string Description,long CreatedByUserID);

}
