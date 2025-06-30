using FluentValidation;
using Hive.Domain.Validation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Hive.API.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, message) = GetExceptionDetails(exception);
            _logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(message, cancellationToken);
            return true;
        }

        private (HttpStatusCode statusCode, object responseBody) GetExceptionDetails(Exception exception)
        {
            return exception switch
            {

                ValidationException validationException =>
                (
                    HttpStatusCode.BadRequest, new { Errors = validationException.Errors.Select(e => e.ErrorMessage) }
                ),

                DomainExceptionValidation => (HttpStatusCode.BadRequest, new { Errors = new[] { exception.Message } }),
               
                _ => (HttpStatusCode.InternalServerError, $"An error unexpected occurred: {exception.Message}"),

            };

        }
    }
}
