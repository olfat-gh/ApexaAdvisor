

using Apexa.AdvisorApp.Application.Common.Exceptions;
using Apexa.AdvisorApp.Contracts.Common;
using Asp.Versioning;
using System.Net;
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

            var httpStatusCode = StatusCodes.Status500InternalServerError;
            var result =  new ErrorApiResponse();

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = StatusCodes.Status400BadRequest;
                    result.FailedMessages=validationException.ValdationErrors;
                    break;
            
                case NotFoundException:
                    httpStatusCode = StatusCodes.Status404NotFound;
                    break;
                case Exception:
                    httpStatusCode  = StatusCodes.Status500InternalServerError;
                    break;
            }
            context.Response.StatusCode = httpStatusCode;
            if (!result.FailedMessages.Any())
                result.FailedMessages.Add(exception.Message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(result));


        }

    }
}