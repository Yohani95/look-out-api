using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.entities.factura;
using look.domain.entities.proyecto;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.soporte
{
    public class Soporte
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

        public int? idEmpresaPrestadora { get; set; }
        public double? ValorHoraAdicional { get; set; }
        public double? ValorHora { get; set; }
        public bool? AcumularHoras { get; set; }
        public double? NumeroHoras{ get; set; }

        public int? IdTipoSoporte { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual EstadoProyecto? EsProy { get; set; }

        public virtual Moneda? Mon { get; set; }

        public virtual TipoServicio? TipoServicio { get; set; }

        public virtual Pais? Pais { get; set; }

        public virtual EmpresaPrestadora? EmpresaPrestadora { get; set; }
        public virtual DiaPagos? DiaPagos { get; set;}
        public List<DocumentosSoporte>? DocumentosSoporte { get; set; }
        [ForeignKey(nameof(kamId))]
        public Persona? personaKam { get; set; }

    }
}
