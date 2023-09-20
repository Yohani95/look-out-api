using look.domain.entities.admin;
using look.domain.entities.cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.dto.cuentas
{
    public class CuentaDTO
    {
        public string email { get; set; }
        public Cliente Cliente { get; set; }
    }
}
