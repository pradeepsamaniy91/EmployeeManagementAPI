using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementInterface.API.Attributes
{
    public class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public RoleAttribute(string role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            // Optimized header read (no LINQ, no Split, no Last)
            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string headerValue = authHeader.ToString();

            if (string.IsNullOrEmpty(headerValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Expected format: "Bearer token"
            const string bearerPrefix = "Bearer ";

            if (!headerValue.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string token = headerValue.Substring(bearerPrefix.Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var key = Encoding.UTF8.GetBytes("superSecretKey@3456543hfjklsujhfkgflglfj");

                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero   // ✅ faster validation
                    },
                    out SecurityToken validatedToken
                );

                var jwtToken = (JwtSecurityToken)validatedToken;

                var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

                if (roleClaim == null || roleClaim.Value != _role)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedResult();
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
