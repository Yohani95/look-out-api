using look.domain.entities.admin;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.cuentas
{
    public class Cliente
    {
        [Key]
        public int CliId { get; set; }

        public string? CliNombre { get; set; }

        public string? CliDescripcion { get; set; }

        public int? EclId { get; set; }

        public int PaiId { get; set; }

        public int? SecId { get; set; }

        public int? GirId { get; set; }

        public string? CliSitioWeb { get; set; }
        public string? CliNif { get; set; }

        public virtual EstadoCliente? EstadoCliente { get; set; }

        public virtual Giro? Giro { get; set; }

        public virtual Pais? Pais { get; set; } = null!;

        public virtual SectorComercial? SectorComercial { get; set; }
        [NotMapped] // Evita que Entity Framework lo trate como columna
        public virtual Persona? Kam { get; set; }

    }
}
