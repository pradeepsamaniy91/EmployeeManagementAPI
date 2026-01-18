using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagementInterface.API.ModelsView;
using EmployeeManagementInterface.API.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementInterface.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// This controller Need to correct dont expose the DbContext directly
        /// </summary>
        private readonly EmployeeManagementContext _employeeManagementContext;
        private readonly ITokenService _tokenService;
        public AuthController(EmployeeManagementContext employeeManagementContext, ITokenService tokenService)
        {
            _employeeManagementContext = employeeManagementContext ?? throw new ArgumentNullException(nameof(employeeManagementContext));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = from e in _employeeManagementContext.Employees
                       join u in _employeeManagementContext.Users on e.EmpId equals u.EmpId
                       where e.EmailId == loginModel.UserEmail && e.Password == loginModel.Password && e.IsActive == "1"
                       select new { e.EmpId, e.EmailId, u.IsActive, u.UserTypeId, };


            if (user is null)
                return BadRequest("Invalid username or password"); 

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginModel.UserEmail),
            new Claim(ClaimTypes.Role, user?.FirstOrDefault()?.UserTypeId?.ToString())
        };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var userDetail = _employeeManagementContext.Users.FirstOrDefault(r => r.Email == loginModel.UserEmail);
            if (userDetail is not null)
            {

                userDetail.RefreshToken = refreshToken;
                userDetail.TokenExpirationTime = DateTime.Now.AddDays(1);
                _employeeManagementContext.Users.Update(userDetail);
                _employeeManagementContext.SaveChanges();
            }
           
            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Role = user.FirstOrDefault()?.UserTypeId.ToString()
            });
        }
       
    }
}
