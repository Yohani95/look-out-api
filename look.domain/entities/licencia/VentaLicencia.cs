using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.licencia
{
    public class VentaLicencia
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaCierre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaRenovacion { get; set; }
        public int? IdEstado { get; set; }
        public int? IdCliente { get; set; }
        public int? IdContacto { get; set; }
        public int? IdKam { get; set; }
        public int? IdMoneda { get; set; }
        public double? Monto { get; set; }
        public int? IdPais { get; set; }
        public int? IdTipoFacturacion { get; set; }
        public int? idTipoLicencia { get; set; }
    }
}
