using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface ITimeSheet
    {
        Task<IEnumerable<object?>> GetTimesheetAsync(int month,int year,CancellationToken cancellationToken);
        Task<WorkLog?> GetTimeByUserIDandWorkDateAsync(long userId, DateOnly workDate, CancellationToken cancellationToken);
        Task<WorkLog> AddUserTimeAsync(WorkLog workLog,CancellationToken cancellationToken);
        Task<WorkLog?> UpdateUserTimeAsync(WorkLog workLog,CancellationToken cancellationToken);
        Task<bool> DeleteUserTimeAsync(long workLogId,CancellationToken cancellationToken);
    }
}
