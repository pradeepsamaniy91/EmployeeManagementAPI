using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Dto.AddTime
{
    public class AddUserTimeDtoValidator:AbstractValidator<AddUserTimeDto>
    {
        public AddUserTimeDtoValidator()
        {
            RuleFor(x => x.userId).NotNull().WithMessage("User Id is required");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.HoursWorked).InclusiveBetween(1, 24).WithMessage("Houre must be 1 to 24");
            RuleFor(x => x.CreatedByUserID).NotEmpty();
        }
    }
}
