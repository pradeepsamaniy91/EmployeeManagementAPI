using EmployeeManagement.Application.Commands.AddTime;
using EmployeeManagement.Application.Dto.AddTime;
using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Application.Queries.GetTimesheet;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementInterface.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddUserTimeDto> _validator;
        public TimesheetController(IMediator mediator, IValidator<AddUserTimeDto> validator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet("EmployeeByEmailId")]
        public async Task<IActionResult> Get(string emailAddress)
        {
            var employeeResult = await _mediator.Send(new GetEmployeeByIdQuery(emailAddress));
            
            return employeeResult.IsSuccess? Ok(employeeResult.Value): BadRequest(employeeResult.Error);
        }
        [HttpGet("Timesheet")]
        public async Task<IActionResult> GetUsersTimesheet(int month,int year,CancellationToken cancellationToken)
        {
            var employeeResult = await _mediator.Send(new GetUserTimesheetQuery(month,year));

            return employeeResult.IsSuccess ? Ok(employeeResult.Value) : BadRequest(employeeResult.Error);
        }
        [HttpPost("AddTime")]
        public async Task<IActionResult> AddTime(AddUserTimeDto request)
        {
            var validatAddTimeDto=_validator.Validate(request);  // Fluent Validation Applied
            if (!validatAddTimeDto.IsValid)
            {
                return BadRequest(validatAddTimeDto);
            }

            var employeeResult = await _mediator.Send(new AddUserTimeCommand(request));

            return employeeResult.IsSuccess ? Ok("Time saved succesfully") : BadRequest(employeeResult);
        }
    }
}
