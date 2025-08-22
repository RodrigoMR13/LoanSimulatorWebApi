using Application.OpenTelemetry;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Infrastructure.Middlewares
{
    public class TelemetryMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TelemetryStorage _storage;

        public TelemetryMiddleware(RequestDelegate next, TelemetryStorage storage)
        {
            _next = next;
            _storage = storage;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            bool success = true;

            try
            {
                await _next(context);
                if (context.Response.StatusCode >= 400)
                    success = false;
            }
            catch
            {
                success = false;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _storage.Record(context.Request.Path, stopwatch.ElapsedMilliseconds, success);
            }
        }
    }
}
