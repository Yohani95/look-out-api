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
        public int EmaId { get; set; }

        public int? CliId { get; set; }

        public int? PerId { get; set; }

        public string? EmaEmail { get; set; }

        public int? TemId { get; set; }
        public int? EmaPrincipal { get; set; }

        public sbyte? EmaVigente { get; set; }

        public virtual Cliente? Cli { get; set; }

        public virtual Persona? Per { get; set; }

        public virtual TipoEmail? Tem { get; set; }


    }
}