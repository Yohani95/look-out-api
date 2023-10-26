using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.dto.proyecto
{
    public class ProfesionalesDTO
    {
        public Persona? Persona { get; set; }
        public ProyectoParticipante? Participante { get; set; }
    }
}
