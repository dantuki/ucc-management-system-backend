using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Security;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title, detail) = exception switch
        {
            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                "Resource not found",
                "The requested resource was not found."),
            ArgumentException => (
                StatusCodes.Status400BadRequest,
                "Invalid request",
                "The request contains invalid values."),
            InvalidOperationException => (
                StatusCodes.Status409Conflict,
                "Operation conflict",
                exception.Message),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Unexpected server error",
                "An unexpected error occurred.")
        };
        logger.LogError(exception, "Unhandled request failure. TraceId: {TraceId}", context.TraceIdentifier);
        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
        problem.Extensions["traceId"] = context.TraceIdentifier;
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }
}
