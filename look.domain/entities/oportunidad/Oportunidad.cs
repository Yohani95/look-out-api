using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.entities.factura;
using look.domain.entities.proyecto;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.oportunidad
{
    public class Oportunidad
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaCierre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdEstadoOportunidad {get ;set ;}

        public int? IdCliente { get; set; }
        public int? IdMoneda { get; set; }
        public double? Monto { get; set; }
        public int? IdTipoOportunidad { get; set; }
        public int? IdPais { get; set; }
        public bool? Renovable { get; set; }
        /// <summary>
        /// fue licitado?
        /// </summary>
        public int? IdLicitacion { get; set; }
        public DateTime? FechaRenovacion { get; set; }
        public int? IdEmpresaPrestadora { get; set; }
        public int? IdAreaServicio { get; set; }
        public int? IdContacto { get; set; }
        public int? IdKam { get; set; }
        public string? Descripcion { get; set; }
        public int? IdOrigen{ get; set; }
        public int? IdTipoLicencia{ get; set; }
        public int? IdTipoCerrada{ get; set; }
        public virtual List<DocumentoOportunidad>? DocumentosOportunidad{ get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Moneda? Moneda { get; set; }
        public virtual EmpresaPrestadora? EmpresaPrestadora { get; set; }
        public virtual Pais? Pais { get; set; }
        public virtual TipoOportunidad? TipoOportunidad{ get; set; }
        public virtual EstadoOportunidad? EstadoOportunidad{ get; set; }
        public virtual Persona? PersonaKam{ get; set; }

    }
}
