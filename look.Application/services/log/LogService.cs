using look.Application.interfaces.log;
using look.domain.entities.Common;
using look.domain.interfaces;
using look.domain.interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.log
{
    public class LogService : Service<LogEntry>, ILogService
    {
        public LogService(ILogRepository repository) : base(repository)
        {
        }
    }
}
