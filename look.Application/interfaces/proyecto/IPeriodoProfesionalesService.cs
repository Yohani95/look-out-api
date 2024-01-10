using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.proyecto
{
    public interface IPeriodoProfesionalesService:IService<PeriodoProfesionales>
    {
        Task<List<PeriodoProfesionales>> GetAllEntitiesByIdPeriod(int idPeriodo);
    }
}
