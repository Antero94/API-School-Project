using System.Diagnostics;

namespace StudentBloggAPI.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Noe gikk galt - {@Machine} {@TraceId}",
                Environment.MachineName,
                Activity.Current?.Id);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await Results.Problem(
                title: "Noe gikk galt!",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, object?>
                {
                    { "traceId", Activity.Current?.Id },
                }).ExecuteAsync(context);
        }
    }
}
