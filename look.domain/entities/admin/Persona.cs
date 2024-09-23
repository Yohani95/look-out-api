using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.admin
{
    public class Persona
    {
        public int Id { get; set; }

        public string? PerIdNacional { get; set; }

        public string? PerNombres { get; set; } = null!;

        public string? PerApellidoPaterno { get; set; } = null!;

        public string? PerApellidoMaterno { get; set; } = null!;

        public int? PaiId { get; set; }

        public int? TpeId { get; set; }
        public string? Cargo { get; set; }

        public DateTime? PerFechaNacimiento { get; set; }
        public TipoPersona? TipoPersona { get; set; }
    }
}
