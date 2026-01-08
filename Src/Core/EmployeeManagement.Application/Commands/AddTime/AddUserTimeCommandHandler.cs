using EmployeeManagement.Application.Commands.AddEmployee;
using EmployeeManagement.Application.Mapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.AddTime
{
    public class AddUserTimeCommandHandler : IRequestHandler<AddUserTimeCommand, Result>
    {
        private readonly ITimeSheet _timesheet;
       
        public AddUserTimeCommandHandler(ITimeSheet timesheet)
        {
            _timesheet = timesheet ?? throw new ArgumentNullException(nameof(timesheet));

        }
        public async Task<Result> Handle(AddUserTimeCommand request, CancellationToken cancellationToken)
        {
            var workLogStatus= await _timesheet.GetTimeByUserIDandWorkDateAsync(request.AddUserTimeDto.userId,request.AddUserTimeDto.Date, cancellationToken);
            if(workLogStatus!=null)
            {
                var updatedworklog=WorkLogsMapper.ToWorkLogUpdate(request.AddUserTimeDto, workLogStatus);
                var worklogstatus=await _timesheet.UpdateUserTimeAsync(updatedworklog,cancellationToken);
                return Result.Failure("successfully updated");
            }
            var worklogMapper = WorkLogsMapper.ToWorkLog(request.AddUserTimeDto);
            WorkLog worklog =await _timesheet.AddUserTimeAsync(worklogMapper, cancellationToken);
            if (worklog != null)
            {
                return Result.Success();
            }
            return Result.Failure("unable to log time");
        }
    }
}
