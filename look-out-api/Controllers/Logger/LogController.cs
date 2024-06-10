
using look.Application.interfaces.log;
using look.domain.entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.Logger
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : BaseController<LogEntry>
    {
        public LogController(ILogService service) : base(service)
        {
        }

        protected override int GetEntityId(LogEntry entity)
        {
            return entity.Id;
        }
    }
}
