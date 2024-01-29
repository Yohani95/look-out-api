using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.factura
{
    public class DocumentosFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public string? NombreDocumento { get; set; }
        public byte[]? ContenidoDocumento { get; set; }

        public virtual FacturaPeriodo? FacturaPeriodo { get; set; }
    }
}
