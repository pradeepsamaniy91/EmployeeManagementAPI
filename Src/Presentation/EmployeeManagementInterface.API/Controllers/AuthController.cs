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
        private readonly EmployeeManagementContext _userContext;
        private readonly ITokenService _tokenService;
        public AuthController(EmployeeManagementContext userContext, ITokenService tokenService)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = from e in _userContext.Employees
                        join u in _userContext.Users on e.EmpId equals u.EmpId
                       where e.EmailId == loginModel.UserEmail && e.Password == loginModel.Password
                        select new { e.EmpId,e.EmailId, u.IsActive,u.UserTypeId, };

            
            if (user.FirstOrDefault().EmailId is null)
                return Unauthorized();

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginModel.UserEmail),
            new Claim(ClaimTypes.Role, user?.FirstOrDefault()?.UserTypeId?.ToString())
        };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var refreshTokenCheck= _userContext.RefreshTokens.FirstOrDefault(r => r.EmailId == loginModel.UserEmail);
            if (refreshTokenCheck == null)
                {
                RefreshToken newRefreshToken = new RefreshToken
                {
                    EmailId = loginModel.UserEmail,
                    RefreshToken1 = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(1)
                };
                _userContext.RefreshTokens.Add(newRefreshToken);
                _userContext.SaveChanges();
            }
            else
            {
                refreshTokenCheck.RefreshToken1 = refreshToken;
                refreshTokenCheck.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
                _userContext.RefreshTokens.Update(refreshTokenCheck);
            }
            //user.RefreshToken = refreshToken;
            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1); //token will expire in a Day
            //_employeeManagementContext.SaveChanges();
            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Role= user.FirstOrDefault()?.UserTypeId.ToString()
            });
        }
    }
}
