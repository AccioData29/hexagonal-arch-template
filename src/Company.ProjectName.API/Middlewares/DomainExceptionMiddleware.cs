namespace Company.ProjectName.API.Middlewares;

public class DomainExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DomainExceptionMiddleware> _logger;

    public DomainExceptionMiddleware(RequestDelegate next, ILogger<DomainExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException ex)
        {
            _logger.LogWarning("Domain exception: {Type} {StatusCode} {Message}",
                ex.GetType().Name, ex.StatusCode, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            var response = ApiResponse<object>.Error(ex.StatusCode, ex.Message, ex.Errors);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
