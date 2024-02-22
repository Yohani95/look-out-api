using look.domain.entities.cuentas;
using look.domain.entities.factura;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.proyecto
{
    public class Proyecto
    {
        [Key]
        public int PryId { get; set; }
        public string? PryNombre { get; set; }
        public int? PrpId { get; set; }
        public int? EpyId { get; set; }
        public int? TseId { get; set; }
        public DateTime? PryFechaInicioEstimada { get; set; }
        public double? PryValor { get; set; }
        public int? MonId { get; set; }
        public int PryIdCliente { get; set; }
        public DateTime? PryFechaCierreEstimada { get; set; }
        public DateTime? PryFechaCierre { get; set; }
        public int? PryIdContacto { get; set; }
        public int? kamId { get; set; }
        public int? PaisId { get; set; }
        public sbyte? FacturacionDiaHabil { get; set; }
        public int? FechaCorte { get; set; }

        public int? idTipoFacturacion { get; set; }

        public int? IdDiaPago { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual EstadoProyecto? EsProy { get; set; }

        public virtual Moneda? Mon { get; set; }

        public virtual TipoServicio? TipoServicio { get; set; }

        public virtual Pais? Pais { get; set; }

    }
}
