using Library.Middleware.Errors;
using Library.Middleware.Exeption;
using System.Text.Json;

namespace Library.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");


                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context,
            Exception exception)
        {
            var statusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,

                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var message = exception switch
            {
                NotFoundException => exception.Message,
                BadRequestException => exception.Message,
                _ => "An unexpected error occurred"
            };
            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message
            };
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
