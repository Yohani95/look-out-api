using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyectoDesarrollo
{
    public class ProyectoDesarrollo
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string? Nombre { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int? IdContacto { get; set; }
        public int? IdKam { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdTipoProyecto { get; set; }
        public int? IdEstado { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEtapa { get; set; }
        public double? avance { get; set; }
        public int? IdPais { get; set; }
        public int? IdEmpresaPrestadora { get; set; }
        public double? Monto { get; set; }
        public virtual EstadoProyectoDesarrollo? Estado { get; set; }
        public virtual Moneda? Moneda { get; set; }
        public virtual EtapaProyectoDesarrollo? Etapa { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual TipoProyectoDesarrollo? TipoProyectoDesarrollo { get; set; }

        public virtual Persona? Kam { get; set; }

    }
}
