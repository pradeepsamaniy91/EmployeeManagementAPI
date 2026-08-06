using EmployeeManagement.Application.Commands.AddTime;
using EmployeeManagement.Application.Dto.AddTime;
using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Application.Queries.GetTimesheet;
using EmployeeManagementInterface.API.Attributes;
using EmployeeManagementInterface.API.ModelsView;
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
        private readonly ILogger<TimesheetController> _iLogger;
        public TimesheetController(IMediator mediator, IValidator<AddUserTimeDto> validator, ILogger<TimesheetController> iLogger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _iLogger = iLogger;
        }
        [Role("1","2")]
        [HttpGet("EmployeeByEmailId")]
        public async Task<IActionResult> Get([FromQuery] string emailAddress)
        {
            _iLogger.LogInformation("Fetching employee details for email: {EmailAddress}", emailAddress);

            var employeeResult = await _mediator.Send(new GetEmployeeByEmailIdQuery(emailAddress));

            _iLogger.LogInformation("Fetching employee details for email: {response}", employeeResult);

            return employeeResult.IsSuccess? Ok(employeeResult.Value): BadRequest(employeeResult.Error);
        }
        [HttpGet("Timesheet")]
        public async Task<IActionResult> GetUsersTimesheet([FromQuery] TimesheetModel request)
        {
            var employeeResult = await _mediator.Send(new GetUserTimesheetQuery(request.Month, request.Year));

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
