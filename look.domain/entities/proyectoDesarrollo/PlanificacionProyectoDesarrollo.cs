using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyectoDesarrollo
{
    public class PlanificacionProyectoDesarrollo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public double? PorcentajeCargaTrabajo { get; set; }
        public int? IdEtapa { get; set; }
        public bool? LineaBase { get; set; }
        public int? IdProyectoDesarrollo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public virtual EtapaProyectoDesarrollo? Etapa { get; set; }
    }
}
