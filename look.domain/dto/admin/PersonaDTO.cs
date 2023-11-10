using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.entities.world;
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
        public List<Email>? Emails { get; set; }
        public List<Telefono>? Telefonos { get; set; }
        public List<Direccion>? direcciones { get; set; }
        public ClientePersona? ClientePersona { get; set; }
    }
}
