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

            //var user = _employeeManagementContext.LoginModels.SingleOrDefault(u => u.UserName == username);
            var refreshT =_employeeManagementContext.RefreshTokens.Where(t=>t.EmailId== username).FirstOrDefault();

            if (refreshT is null || refreshT.RefreshToken1 != refreshToken || refreshT.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();


            refreshT.RefreshToken1 = newRefreshToken;
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

            var user = _employeeManagementContext.RefreshTokens.SingleOrDefault(u => u.EmailId == username);
            if (user == null) return BadRequest();

            user.RefreshToken1 = null;

            _employeeManagementContext.SaveChanges();

            return NoContent();
        }
    }
}
