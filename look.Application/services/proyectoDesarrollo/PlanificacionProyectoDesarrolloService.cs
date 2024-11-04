using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.Common;
using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces;
using look.domain.interfaces.proyectoDesarrollo;
using look.domain.interfaces.unitOfWork;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.proyectoDesarrollo
{
    public class PlanificacionProyectoDesarrolloService : Service<PlanificacionProyectoDesarrollo>, IPlanificacionProyectoDesarrolloService
    {
        private readonly IPlanificacionProyectoDesarrolloRepository _PlanificacionProyectoDesarrolloRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger = Logger.GetLogger();
        public PlanificacionProyectoDesarrolloService(IPlanificacionProyectoDesarrolloRepository repository, IUnitOfWork unitOfWork) : base(repository)
        {
            _PlanificacionProyectoDesarrolloRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PlanificacionProyectoDesarrollo>> GetAllByIdProyecto(int id)
        {
            try
            {
                return await _PlanificacionProyectoDesarrolloRepository.GetAllByIdProyecto(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor, e);
                return null;
            }
        }
    }
}
