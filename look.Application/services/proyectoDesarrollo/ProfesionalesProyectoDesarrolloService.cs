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
    public class ProfesionalesProyectoDesarrolloService : Service<ProfesionalesProyectoDesarrollo>, IProfesionalesProyectoDesarrolloService
    {
        private readonly IProfesionalesProyectoDesarrolloRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger = Logger.GetLogger();
        public ProfesionalesProyectoDesarrolloService(IProfesionalesProyectoDesarrolloRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<ProfesionalesProyectoDesarrollo>> GetByProyectoDesarrolloId(int id)
        {
            try
            {
                return await _repository.GetByProyectoDesarrolloId(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e);
                return null;
            }
        }
    }
}
