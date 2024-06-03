using look.domain.entities.Common;
using look.domain.interfaces.Common;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.Logger
{
    public class LogRepository :Repository<LogEntry> ,ILogRepository
    {
        public LogRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

    }
}
