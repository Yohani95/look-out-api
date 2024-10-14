using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyectoDesarrollo
{
    public class HitoProyectoDesarrollo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdProyectoDesarrollo { get; set; }
        public int? idTipoPagoHito { get; set; }
        public double? Monto { get; set; }
        public double? PorcentajePagado { get; set; }
        public string? Descripcion { get; set; }

        public virtual TipoHitoProyectoDesarrollo? TipoHitoProyectoDesarrollo { get; set; }
    }
}
