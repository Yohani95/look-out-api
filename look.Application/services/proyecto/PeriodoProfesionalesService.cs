using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.proyecto
{
    public class PeriodoProfesionalesService : Service<PeriodoProfesionales>, IPeriodoProfesionalesService
    {
        private readonly IPeriodoProfesionalesRepository _periodoRepository;
        public PeriodoProfesionalesService(IPeriodoProfesionalesRepository periodoRepository) : base(periodoRepository)
        {
            _periodoRepository = periodoRepository;
        }

        public Task<List<PeriodoProfesionales>> GetAllEntitiesByIdPeriod(int idPeriodo)
        {
            return _periodoRepository.GetAllEntitiesByIdPeriod(idPeriodo);
        }
    }
}
