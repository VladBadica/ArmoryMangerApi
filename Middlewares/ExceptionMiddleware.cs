using ArmoryManagerApi.Errors;
using System.Net;

namespace ArmoryManagerApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware (RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
            _next = next;
            _logger = logger;
        }  
        
        public async Task Invoke (HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                HttpStatusCode statusCode;
                string message;
                var exceptionType = ex.GetType();

                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = "You are not authorized";
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An error occured";
                }

                var response = new ApiError((int) statusCode, message);

                _logger.LogError(ex, message);
                context.Response.StatusCode = (int) statusCode;
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
