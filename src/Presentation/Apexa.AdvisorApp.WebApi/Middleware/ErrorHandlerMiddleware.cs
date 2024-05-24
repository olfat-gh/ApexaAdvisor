

using Apexa.AdvisorApp.Application.Common.Exceptions;
using Asp.Versioning;
using System.Text.Json;

namespace Apexa.AdvisorApp.WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiVersionReader _apiVersionReader;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, IApiVersionReader apiVersionReader
        , ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _apiVersionReader = apiVersionReader;
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            _logger.LogError(exception, exception.Message);
  
            context.Response.StatusCode = exception switch 
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError 
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            { 
                StatusCode = context.Response.StatusCode, 
                Message = exception.Message, 
            }));


        }

    }
}