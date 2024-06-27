using look.domain.entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.Common
{
    public interface ILogRepository:IRepository<LogEntry>
    {
    }
}
