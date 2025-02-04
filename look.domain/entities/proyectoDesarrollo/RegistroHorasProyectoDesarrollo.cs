using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyectoDesarrollo
{
    public class RegistroHorasProyectoDesarrollo
    {
        public int Id { get; set; }
        public int IdProfesionalProyecto { get; set; }
        public DateTime Semana { get; set; } // Se guarda la fecha del primer día de la semana
        public double HorasTrabajadas { get; set; } // Ahora es double para admitir decimales

        public virtual ProfesionalesProyectoDesarrollo? ProfesionalProyecto { get; set; }
    }
}
