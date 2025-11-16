using Microsoft.AspNetCore.Mvc;

namespace OrderManagementSystem.Presentation.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = "An unhandled exception occurred.",
                    Detail = ex.Message
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
