using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.factura
{
    public class FacturaPeriodo
    {
        public int Id { get; set; }
        public string? Rut { get; set; }
        public string? RazonSocial { get; set; }
        public string? HesCodigo { get; set; }
        public string? OcCodigo { get; set; }
        public DateTime? FechaHes { get; set; }
        public DateTime? FechaOc { get; set; }
        public int? OrdenPeriodo { get; set; }
        public string? Observaciones { get; set; }
        public int? IdPeriodo { get; set; }
        public double? Monto { get; set; }
        public DateTime? FechaFactura { get; set; }
        public virtual PeriodoProyecto? Periodo { get; set; }
    }
}
