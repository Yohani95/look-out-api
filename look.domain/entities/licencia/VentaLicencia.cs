using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.entities.factura;
using look.domain.entities.oportunidad;
using look.domain.entities.proyecto;
using look.domain.entities.world;
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
        public int? IdEmpresaPrestadora { get; set; }
        public int? Descuento { get; set; }
        public int? IdDiaPago { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual Moneda? Moneda { get; set; }
        public virtual EmpresaPrestadora? EmpresaPrestadora { get; set; }
        public virtual Pais? Pais { get; set; }
        public virtual EstadoVentaLicencia? EstadoVentaLicencia { get; set; }
        //public virtual TipoLicenciaOportunidad? TipoLicencia { get; set; }
        public virtual Persona? Kam { get; set; }

        public virtual DiaPagos? DiaPagos { get; set; }
    }
}
