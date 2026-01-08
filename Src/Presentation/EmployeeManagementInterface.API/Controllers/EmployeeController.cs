using EmployeeManagement.Application.Commands.AddEmployee;
using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.ValueObjects;
using EmployeeManagementInterface.API.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeManagementInterface.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<EmployeeDto> _validator;
        public EmployeeController(IValidator<EmployeeDto> validator, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }



        [HttpPost("create")]
        public async Task<IActionResult> Employee(EmployeeDto request)
        {
            var validationResult = _validator.Validate(request);  // Fluent Validation Applied
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            

            var employeeResult =await _mediator.Send(new CreateEmployeeCommand(request));

            return employeeResult.IsSuccess ? Ok("Employee created successfully"): BadRequest(employeeResult);
        }
    }
}
