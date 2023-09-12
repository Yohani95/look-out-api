using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using look.domain.entities.cuentas;

namespace look.domain.entities.admin
{
    public class Email
    {
        public int emailId { get; set; }
        public int cliId { get; set; }
        public int perId { get; set; }
        public string? emaEmail { get; set; }
        public int temId { get; set; }
        public sbyte? emaVigente { get; set; }
        
        [JsonIgnore]
        public virtual TipoEmail? tipoEmail { get; set;}
        [JsonIgnore]
        public virtual Persona? persona { get; set;}
        [JsonIgnore]
        public virtual Cliente? cliente { get; set;}
        
        
    }
}