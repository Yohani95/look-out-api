using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.cuentas
{
    public class ClientePersona
    {
        public int? CliId { get; set; }

        public int? PerId { get; set; }

        public int? CarId { get; set; }

        public sbyte? CliVigente { get; set; }

       // public virtual Car? Car { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual Persona? Persona { get; set; }
    }
}
