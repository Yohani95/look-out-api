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
    }
}
