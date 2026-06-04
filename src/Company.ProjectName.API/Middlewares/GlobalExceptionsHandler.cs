using Microsoft.AspNetCore.Diagnostics;

namespace Company.ProjectName.API.Middlewares;

public class GlobalExceptionsHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionsHandler> _logger;

    public GlobalExceptionsHandler(ILogger<GlobalExceptionsHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception");

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = 500;

        var response = ApiResponse<object>.Error(500, "An unexpected error occurred.");
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
