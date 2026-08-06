using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.Employee.AddEmployee;

public record CreateEmployeeResponse(
    bool IsSuccess,
    string EmployeeId,
    string Message
);
