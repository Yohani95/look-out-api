using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.oportunidad
{
    public class NovedadOportunidad
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int IdOportunidad { get; set; }
    }
}
