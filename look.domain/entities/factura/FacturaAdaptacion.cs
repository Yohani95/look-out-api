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
        public int? IdFactura { get; set; }
        public string? Descripcion { get; set; }
        public double? MontoDiferencia { get; set; }

        public FacturaPeriodo? Factura { get; set; }
    }

}
