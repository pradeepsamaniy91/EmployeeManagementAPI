using Microsoft.AspNetCore.Builder;

namespace EmployeeManagement.Application.Exceptions.CustomExceptionMiddleware
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseCustomExceptionHandMethod(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
