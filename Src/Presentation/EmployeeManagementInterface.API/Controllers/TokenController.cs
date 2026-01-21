using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagementInterface.API.ModelsView;
using EmployeeManagementInterface.API.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

namespace EmployeeManagementInterface.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly EmployeeManagementContext _employeeManagementContext;
        private readonly ITokenService _tokenService;

        public TokenController(EmployeeManagementContext employeeManagementContext, ITokenService tokenService)
        {
            this._employeeManagementContext = employeeManagementContext ?? throw new ArgumentNullException(nameof(employeeManagementContext));
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            
            //var user = _employeeManagementContext.LoginModels.SingleOrDefault(u => u.Username == username);
            var refreshT =_employeeManagementContext.Users.Where(t=>t.Email== username).FirstOrDefault();

            if (refreshT is null || refreshT.RefreshToken != refreshToken || refreshT.TokenExpirationTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();


            refreshT.RefreshToken = newRefreshToken;
            _employeeManagementContext.SaveChanges();

            return Ok(new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;

            var user = _employeeManagementContext.Users.SingleOrDefault(u => u.Email == username);
            if (user == null) return BadRequest();

            user.RefreshToken = null;

            _employeeManagementContext.SaveChanges();

            return NoContent();
        }
    }
}
