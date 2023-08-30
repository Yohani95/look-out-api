using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.Common
{
    public class ResponseGeneric<T>
    {
        public ServiceResult serviceResult { get; set; }
        public T? Data { get; set; }
    }
}
