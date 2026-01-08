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
            RuleFor(x => x.userId).NotNull();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.HoursWorked).InclusiveBetween(1, 24);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
            RuleFor(x => x.CreatedByUserID).NotEmpty();
        }
    }
}
