using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.soporte
{
    public class HorasUtilizadas
    {
        public int Id { get; set; }
        public string? NombreDocumento { get; set; }
        public byte[]? ContenidoDocumento { get; set; }
        public double? Horas { get; set; }
        public int? IdSoporte { get; set; }
        public DateTime? FechaPeriodoDesde { get; set; }
        public DateTime? FechaPeriodoHasta { get; set; }
        public int? Estado { get; set; }

        public double? Monto { get; set; }
        public double? MontoHorasExtras { get; set; }
        public double? HorasExtras { get; set; }
        public double? HorasAcumuladas { get; set; }

        public virtual Soporte? Soporte { get; set; }
    }
}
