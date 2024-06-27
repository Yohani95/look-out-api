using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.Common
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Level { get; set; }
        public string? SourceContext { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
