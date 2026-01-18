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
            RuleFor(x => x.LastName).MaximumLength(50);
            RuleFor(x => x.EmailId).NotEmpty().WithMessage("Email address is required.")
            // Rule 2: Ensure the format is a valid email address
            .EmailAddress().WithMessage("Enter valid email address is required.");
            RuleFor(x => x.BillRate).NotNull().WithMessage("BillRate is required");
            RuleFor(x => x.ContactNo).NotEmpty().WithMessage("Contact number is required.");
            RuleFor(x => x.PayRate).NotNull().WithMessage("PayRate is required");
            RuleFor(x => x.UserTypeId).NotNull().WithMessage("User TypeId is require");

            
        }
    }

}
