using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.dto.admin
{
    public  class PersonaDTO
    {
        public Persona Persona { get; set; }
        public int? IdCliente { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Cargo{ get; set;}
        public string? Cuenta { get; set; }
    }
}
