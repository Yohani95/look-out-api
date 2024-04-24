using look.domain.entities.proyecto;
using look.domain.entities.soporte;
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
        public DateTime? FechaVencimiento { get; set; }
        public int? IdEstado { get; set; }

        public int? IdHorasUtilizadas { get; set; }
        public virtual PeriodoProyecto? Periodo { get; set; }
        public virtual HorasUtilizadas? HorasUtilizadas{ get; set; }
        public virtual EstadoFacturaPeriodo? Estado { get; set; }
        public virtual List<DocumentosFactura>? DocumentosFactura { get; set; }
    }
}
