using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UTEvents.Filters
{
    public class ErrorFilter : IExceptionFilter
    {
        private readonly ILogger<ErrorFilter> _logger;

        public ErrorFilter(ILogger<ErrorFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            var response = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Details = context.Exception.Message
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
