using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
namespace look.domain.entities.Common
{
    public class Logger
    {
        private static ILogger? _logger;

        public static void InitializeLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("../logs/log-.txt", rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext}  {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        public static ILogger GetLogger()
        {
            if (_logger == null)
            {
                InitializeLogger();
            }
            return _logger;
        }
    }
}
