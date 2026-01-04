using EmployeeManagement.Application.Queries.GetEmployeeByID;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementInterface.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TimesheetController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("EmployeeByEmailId")]
        public async Task<IActionResult> Get(string emailAddress)
        {
            var employeeResult = await _mediator.Send(new GetEmployeeByIdQuery(emailAddress));
            
            return employeeResult.IsSuccess? Ok(employeeResult.Value): BadRequest(employeeResult.Error);
        }
    }
}
