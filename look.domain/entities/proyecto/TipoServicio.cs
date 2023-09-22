using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyecto
{
    public class TipoServicio
    {
        public int TseId { get; set; }

        public string? TseNombre { get; set; }

        public string? TseDescripcion { get; set; }

        public sbyte? TseVigente { get; set; }
    }
}
