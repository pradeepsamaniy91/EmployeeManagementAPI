using EmployeeManagement.Application.Commands.Employee.AddEmployee;
using EmployeeManagement.Application.Commands.Employee.Remove;
using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Application.Queries.GetEmployeeById;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Application.Queries.GetEmployees;
using EmployeeManagementInterface.API.Attributes;
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
        private readonly ILogger<EmployeeController> _iLogger;
        
        public EmployeeController(IValidator<EmployeeDto> validator, IMediator mediator, ILogger<EmployeeController> iLogger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _iLogger = iLogger;
        }

        [HttpGet("GetEmpByEmailId")]
        [RoleAttribute("1")]
        public async Task<IActionResult> GetByEmailId(string email)
        {
            if (email == null)
            {
                return BadRequest("EmailId is null");
            }
            var employeeResult = await _mediator.Send(new GetEmployeeByEmailIdQuery(email));

            return employeeResult.IsSuccess ? Ok(employeeResult) : BadRequest(employeeResult);
            
        }
        [HttpGet("GetByEmpId")]
        
        public async Task<IActionResult> GetByEmployeeId(long employeeId)
        {
            if (employeeId == null)
            {
                return BadRequest("employee id can't be null");
            }
            var employeeResult = await _mediator.Send(new GetEmployeeByIdQuery(employeeId));

            return employeeResult.IsSuccess ? Ok(employeeResult) : BadRequest(employeeResult);

        }
        [HttpGet("GetEmpployess")]
        public async Task<IActionResult> GetAllEmployess()
        {
            
            var employeeResult = await _mediator.Send(new GetEmployeesQuery());

            return employeeResult.IsSuccess ? Ok(employeeResult) : BadRequest(employeeResult);

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

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveEmployee(int employeeId)
        {
            
            if (employeeId==null)
            {
                return BadRequest("Employee Id is required");
            }
            
            var employeeResult = await _mediator.Send(new RemoveEmployeeCommand(employeeId));
            _iLogger.LogInformation("remove Employee API Mediator Send success.respones................................Pradeep", employeeResult.IsSuccess);
            return employeeResult.IsSuccess ? Ok(employeeResult) : BadRequest(employeeResult);
        }
    }
}
