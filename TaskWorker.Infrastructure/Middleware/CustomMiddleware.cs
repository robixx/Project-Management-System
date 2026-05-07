using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TaskWorker.Infrastructure.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Example: Add custom logic before the next middleware
            await context.Response.WriteAsync("Custom Middleware Executed\n");

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}