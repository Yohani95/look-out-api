using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using look.domain.entities.cuentas;

namespace look.domain.entities.admin
{
    public class Email
    {
        public int emailId { get; set; }
        public int? cliId { get; set; }
        public int? perId { get; set; }
        public string? emaEmail { get; set; }
        public int? temId { get; set; }
        public int? emaVigente { get; set; }
        
        public TipoEmail? tipoEmail { get; set;}
        
        public Persona? persona { get; set;}
        
        public Cliente? cliente { get; set;}
        
        
    }
}
