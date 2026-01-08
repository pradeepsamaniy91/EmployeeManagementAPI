using EmployeeManagement.Application.Dto.AddTime;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Mapper
{
    public static class WorkLogsMapper
    {
        public static WorkLog ToWorkLog(AddUserTimeDto addUserTimeDto)
        {
            return new WorkLog {
                CreatedOn = DateTime.Now, 
                CreatedByUserId = addUserTimeDto.CreatedByUserID, 
                Description = addUserTimeDto.Description, 
                HoursWorked = addUserTimeDto.HoursWorked, 
                UserId = addUserTimeDto.userId, 
                WorkDate = addUserTimeDto.Date,
            };
        }
        public static WorkLog ToWorkLogUpdate(AddUserTimeDto addUserTimeDto, WorkLog workLog)
        {

            workLog.Description = addUserTimeDto.Description;
            workLog.HoursWorked = addUserTimeDto.HoursWorked;
            workLog.UpdatedOn =Convert.ToDateTime(DateTime.Now);
            workLog.UpdatedByUserId = addUserTimeDto.UpdatedByUserId;
            return workLog;
        }
    }
}
