using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace EmployeeManagement.Infrastructure.Persistence.Repositories
{
    public class TimesheetService : ITimeSheet
    {
        private readonly EmployeeManagementContext _context;
        public TimesheetService(EmployeeManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetTimesheetAsync(int month, int year, CancellationToken cancellationToken = default)
        {
            DateOnly startDate = new DateOnly(year, month, 1);
            DateOnly endDate = startDate.AddMonths(1).AddDays(-1);
            var rawData = await _context.Users
                        .Where(u => u.IsActive)
                         .Select(u => new
                         {
                             u.DisplayName,
                             u.UserId,
                             Logs = u.WorkLogs
                        .Where(w => w.WorkDate >= startDate && w.WorkDate <= endDate)
                        .Select(w => new { w.WorkDate, w.HoursWorked })
                        .ToList()
                         })
                        .OrderBy(u => u.DisplayName)
                        .ToListAsync();

            // 3. Generate the list of all dates in the month for columns
            var dateList = Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => startDate.AddDays(day))
                .ToList();

            // 4. Transform to PIVOT format (Memory-side)
            var pivotedData = rawData.Select(u =>
            {
                // Create a dictionary for flexible column names
                var row = new Dictionary<string, object>
    {
        { "Name", u.DisplayName },
        { "UserId", u.UserId }
    };

                decimal totalHours = 0;

                foreach (var date in dateList)
                {
                    // Format the date key exactly like your SQL: "yyyy-MM-dd ddd"
                    string dateKey = date.ToString("yyyy-MM-dd ddd", CultureInfo.InvariantCulture);

                    // Sum hours for this specific day
                    var hours = u.Logs
                        .Where(l => l.WorkDate == date)
                        .Sum(l => (decimal?)l.HoursWorked) ?? 0;

                    row.Add(dateKey, hours);
                    totalHours += hours;
                }

                row.Add("Total", totalHours);
                return row;
            }).ToList();
            return pivotedData;
        }
        public async Task<WorkLog?> GetTimeByUserIDandWorkDateAsync(long userId, DateOnly workDate, CancellationToken cancellationToken)
        {
            return await _context.WorkLogs
                .FirstOrDefaultAsync(w => w.UserId == userId && w.WorkDate == workDate, cancellationToken);
        }


        public async Task<WorkLog> AddUserTimeAsync(WorkLog workLog, CancellationToken cancellationToken)
        {
           await _context.AddAsync(workLog);
           await _context.SaveChangesAsync();
            return workLog;
        }

        Task<bool> ITimeSheet.DeleteUserTimeAsync(long workLogId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkLog?> UpdateUserTimeAsync(WorkLog workLog, CancellationToken cancellationToken)
        {
            _context.WorkLogs.Update(workLog);
            await _context.SaveChangesAsync(cancellationToken);

            return workLog;
        }
    }
}
