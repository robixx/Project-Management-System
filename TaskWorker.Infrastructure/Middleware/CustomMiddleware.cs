using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TaskWorker.Infrastructure.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Avoid writing to the response body for static files, swagger or hub endpoints
            var path = context.Request.Path.Value ?? string.Empty;
            if (path.StartsWith("/swagger") || path.StartsWith("/projectHub") || path.Contains(".svg") || path.Contains(".js") || path.Contains(".css") || path.StartsWith("/favicon.ico"))
            {
                // Let the request continue without modifying the response
                await _next(context);
                return;
            }

            // Log the request for diagnostics instead of writing into the response
            _logger.LogDebug("CustomMiddleware invoked for {Path}", path);

            await _next(context);
        }
    }
}
