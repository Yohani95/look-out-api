using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.admin
{
    public class Usuario
    {
        public int UsuId { get; set; }

        public int? PerId { get; set; }

        public int? PrfId { get; set; }

        public string? UsuNombre { get; set; }

        public string? UsuContraseña { get; set; }

        public sbyte? UsuVigente { get; set; }
        public int? RolId { get; set; }
        public virtual Perfil? Perfil { get; set; }
        public virtual Persona? Persona { get; set; }   
        public virtual Rol? Rol { get; set; }

    }
}
