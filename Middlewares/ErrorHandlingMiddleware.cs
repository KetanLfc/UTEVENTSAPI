using System.Net;
using System.Text.Json;

namespace UTEvents.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed to the next middleware or endpoint
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred."); // Log the error
                await HandleExceptionAsync(context, ex); // Handle the exception
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set default status code and error message
            var statusCode = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new
            {
                error = "An unexpected error occurred. Please try again later."
            });

            // Handle specific exception types if needed (e.g., invalid input)
            if (exception is ArgumentNullException || exception is InvalidOperationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
