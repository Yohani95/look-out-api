using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class DiasFeriados
    {
        public int Id {  get; set; }
        public DateTime? Fecha { get; set; }
        public string? Nombre { get; set; }
        public string? Pais { get; set; }
        public string? Tipo { get; set; }
        public int? IdPais { get; set; }
    }
}
