using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyectoDesarrollo
{
    public class ProfesionalesProyectoDesarrollo
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int IdPersona { get; set; }
        public int IdProyectoDesarrollo { get; set; }
        public virtual Persona? Persona { get; set; }
    }
}
