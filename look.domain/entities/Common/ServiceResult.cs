using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.Common
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public ServiceResultMessage MessageCode { get; set; }
        public string? Message { get; set; }

        public List<string>? Errors { get; set; } // Lista para almacenar los errores
    }
}
