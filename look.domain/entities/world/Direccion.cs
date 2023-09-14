using look.domain.entities.admin;
using look.domain.entities.cuentas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class Direccion
    {
        public int DirId { get; set; }

        public int? PerId { get; set; }

        public int? CliId { get; set; }

        public string? DirCalle { get; set; }

        public string? DirNumero { get; set; }

        public int? ComId { get; set; }

        public string? DirBlock { get; set; }

        public int? TdiId { get; set; }
        
        public virtual Cliente? Cli { get; set; }
        public virtual Comuna? Com { get; set; }
        public virtual Persona? Per { get; set; }
        public virtual TipoDireccion? Tdi { get; set; }
    }
}
