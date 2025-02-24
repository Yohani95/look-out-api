using look.domain.dto.admin;
using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.dto.proyecto
{
    public class PeriodoProyectoDto
    {
        public int Id { get; set; }
        public int PryId { get; set; }
        public DateTime FechaPeriodoDesde { get; set; }
        public DateTime FechaPeriodoHasta { get; set; }
        public decimal Monto { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}
