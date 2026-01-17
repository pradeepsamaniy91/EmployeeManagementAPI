using EmployeeManagementInterface.API.ModelsView;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementInterface.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        [HttpPost("Login")]
        public void Post([FromBody] LoginModel loginModel)
        {

        }       
    }
}
