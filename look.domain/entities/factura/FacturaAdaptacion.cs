using look.domain.entities.cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.factura
{
    public class FacturaAdaptacion
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public double? Monto { get; set; }
        public int? IdCliente { get; set; }
        public string? Descripcion { get; set; }
        public double? MontoDiferencia { get; set; }
        public int? IdPeriodoProyecto { get; set; }
        public int? IdSoporte { get; set; }
        public int? IdLicencia { get; set; }
        public int? IdHorasUtilizadas { get; set; }
        public int? IdHitoProyectoDesarrollo { get; set; }
        public bool? Solicitada { get; set; }
        public Cliente? Cliente { get; set; }
    }

}
