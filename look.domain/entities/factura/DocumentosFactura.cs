using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.factura
{
    public class DocumentosFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public string? NombreDocumento { get; set; }
        public byte[]? ContenidoDocumento { get; set; }
        public double? Monto { get; set; }
        public int? IdtipoMoneda { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual FacturaPeriodo? FacturaPeriodo { get; set; }
    }
}
