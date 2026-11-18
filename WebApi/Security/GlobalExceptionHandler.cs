using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Security;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var status = exception is InvalidOperationException ? StatusCodes.Status409Conflict : StatusCodes.Status500InternalServerError;
        logger.LogError(exception, "Unhandled request failure. TraceId: {TraceId}", context.TraceIdentifier);
        var problem = new ProblemDetails
        {
            Status = status,
            Title = status == StatusCodes.Status409Conflict ? "Operation conflict" : "Unexpected server error",
            Detail = status == StatusCodes.Status409Conflict ? exception.Message : "An unexpected error occurred.",
            Instance = context.Request.Path
        };
        problem.Extensions["traceId"] = context.TraceIdentifier;
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }
}
