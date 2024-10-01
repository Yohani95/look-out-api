using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.prospecto
{
    public class TipoContactoProspecto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(200)]
        public string? Descripcion { get; set; }

        // las constantes como propiedades estáticas
        public static readonly TipoContactoProspecto TIR1 = new TipoContactoProspecto { Id = 1, Nombre = "TIR1" };
        public static readonly TipoContactoProspecto TIR2 = new TipoContactoProspecto { Id = 2, Nombre = "TIR2" };
        public static readonly TipoContactoProspecto TIR3 = new TipoContactoProspecto { Id = 3, Nombre = "TIR3" };
    }
}
