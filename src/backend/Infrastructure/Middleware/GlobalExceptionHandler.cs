using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            switch (exception)
            {
                case FluentValidation.ValidationException fluentException:
                    problemDetails.Title = "One or more validation errors occurred.";
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    IEnumerable<string> validationErrors = fluentException.Errors.Select(x => x.ErrorMessage).ToList();
                    problemDetails.Extensions.Add("errors", validationErrors);
                    break;
                case ValidationException validationException:
                    problemDetails.Title = validationException.Message;
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    problemDetails.Extensions.Add("error", validationException.Error);
                    break;
                case ConflictException conflictException:
                    problemDetails.Title = "Conflict error occurred.";
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    problemDetails.Detail = exception.Message;
                    break;
                case NotFoundException NotFoundException:
                    problemDetails.Title = "NotFound error occurred.";
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    problemDetails.Detail = exception.Message;
                    break;
                case UploadImageException UploadImageException:
                    problemDetails.Title = "Upload error occurred.";
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = exception.Message;
                    break;
                default:
                    problemDetails.Title = exception.Message;
                    break;
            }
            _logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);
            problemDetails.Status = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
