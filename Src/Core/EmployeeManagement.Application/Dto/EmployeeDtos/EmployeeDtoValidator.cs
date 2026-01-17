using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Dto.EmployeeDtos
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.EmailId).EmailAddress();
            RuleFor(x => x.BillRate);
            RuleFor(x => x.ContactNo).NotNull();
            RuleFor(x => x.PayRate).NotNull();
            RuleFor(x => x.PayRate).NotNull();
        }
    }

}
