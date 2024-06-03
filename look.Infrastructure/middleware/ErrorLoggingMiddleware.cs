using look.domain.interfaces.Common;
using Serilog;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;
using look.domain.entities.Common;
using Microsoft.Extensions.DependencyInjection;

namespace look.Infrastructure.middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ErrorLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var logRepository = scope.ServiceProvider.GetRequiredService<ILogRepository>();

                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Log.Error(ex, "Unhandled exception caught");

                    // Save the exception to the database
                    await SaveExceptionToDatabase(logRepository, ex);

                    // Rethrow the exception to let the global exception handler handle it
                    throw;
                }
            }
        }

        private async Task SaveExceptionToDatabase(ILogRepository logRepository, Exception ex)
        {
            // Create a log entry with the exception details
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Level = "Error",
                SourceContext = "Global",
                Message = "Unhandled exception caught",
                Exception = ex.ToString()
            };

            // Save the log entry to the database
            await logRepository.AddAsync(logEntry);
        }
    }
}
