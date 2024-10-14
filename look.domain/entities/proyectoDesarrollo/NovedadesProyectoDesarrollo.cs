using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyectoDesarrollo
{
    public class NovedadesProyectoDesarrollo
    {
        public int Id { get; set; }
        public string? nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdProyectoDesarrollo { get; set; }
        public int? IdTipoNovedadProyectoDesarrollo { get; set; }

        public string? Descripcion { get; set; }

        public int? IdKam { get; set; }

        public virtual TipoNovedadProyectoDesarrollo? TipoNovedad { get; set; }
        public virtual Persona? Persona { get; set; }
    }
}
