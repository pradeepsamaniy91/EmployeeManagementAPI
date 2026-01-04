using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Commands.AddEmployee;

public record CreateEmployeeResponse(
    bool Success,
    string EmployeeId,
    string EmailAddress
);
