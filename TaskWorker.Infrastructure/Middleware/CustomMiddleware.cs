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
            
            var path = context.Request.Path.Value ?? string.Empty;

            if (path.StartsWith("/swagger") || path.StartsWith("/projectHub") || path.Contains(".svg") || path.Contains(".js") || path.Contains(".css") || path.StartsWith("/favicon.ico"))
            {
                
                await _next(context);
                return;
            }

            
            _logger.LogDebug("CustomMiddleware invoked for {Path}", path);

            await _next(context);
        }
    }
}
