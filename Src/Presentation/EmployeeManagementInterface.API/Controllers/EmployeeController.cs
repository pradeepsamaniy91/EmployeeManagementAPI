using EmployeeManagement.Application.Commands.AddEmployee;
using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Domain.ValueObjects;
using EmployeeManagementInterface.API.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeManagementInterface.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<EmployeeDto> _validator;
        private readonly ILogger<EmployeeController> _iLogger;
        
        public EmployeeController(IValidator<EmployeeDto> validator, IMediator mediator, ILogger<EmployeeController> iLogger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _iLogger = iLogger;
        }
        [HttpGet("Test")]
        public async Task<IActionResult> Get()
        {
            _iLogger.LogInformation("Test API called.................................Pradeep");

            return Ok("Sucess");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Employee(EmployeeDto request)
        {
            _iLogger.LogInformation("Create Employee API called.................................Pradeep");
            var validationResult = _validator.Validate(request);  // Fluent Validation Applied
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            _iLogger.LogInformation("Create Employee API validation success.................................Pradeep");
            var employeeResult = await _mediator.Send(new CreateEmployeeCommand(request));
            _iLogger.LogInformation("Create Employee API Mediator Send success.respones................................Pradeep",employeeResult.IsSuccess);
            return employeeResult.IsSuccess ? Ok(employeeResult) : BadRequest(employeeResult);
        }
    }
}
