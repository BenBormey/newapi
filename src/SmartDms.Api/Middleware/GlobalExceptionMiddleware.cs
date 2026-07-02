using System.Net;
using System.Text.Json;

namespace JuJuBis.Api.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var status = ex switch
            {
                ArgumentException     => HttpStatusCode.BadRequest,
                FileNotFoundException => HttpStatusCode.NotFound,
                _                     => HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                timestamp = DateTime.UtcNow,
                status = (int)status,
                error = ex.Message
            }));
        }
    }
}
