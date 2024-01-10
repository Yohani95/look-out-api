using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyecto
{
    public class PeriodoProfesionales
    {
        public int Id { get; set; }
        public int? DiasTrabajados { get; set; }
        public int?  DiasAusentes { get; set; }
        public int? DiasFeriados { get; set; }
        public int? DiasVacaciones { get; set; }
        public int? DiasLicencia { get; set; }
        public int? IdPeriodo { get; set; }
        public int? IdParticipante { get; set; }
        public double? MontoDiario { get; set; }
        public double? MontoTotalPagado { get; set; }
        public virtual PeriodoProyecto? Periodo { get; set; }
        public virtual ProyectoParticipante? Participante { get; set; }

    }
}
