using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;


namespace EmployeeManagement.Application.Exceptions.CustomExceptionMiddleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _log;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var watch = Stopwatch.StartNew();
                await _next.Invoke(context);
                watch.Stop();
                var responseTime = watch.ElapsedMilliseconds;

                Console.WriteLine(responseTime.ToString());
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            //context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
