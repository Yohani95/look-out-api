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
    public class NovedadesProyectoDesarrolloService : Service<NovedadesProyectoDesarrollo>, INovedadesProyectoDesarrolloService
    {
        private readonly INovedadesProyectoDesarrolloRepository _NovedadesProyectoDesarrolloRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger = Logger.GetLogger();
        public NovedadesProyectoDesarrolloService(INovedadesProyectoDesarrolloRepository repository, IUnitOfWork unitOfWork) : base(repository)
        {
            _unitOfWork = unitOfWork;
            _NovedadesProyectoDesarrolloRepository = repository;
        }

        public async Task<List<NovedadesProyectoDesarrollo>> GetAllByIdProyecto(int id)
        {
            try
            {
                return await _NovedadesProyectoDesarrolloRepository.GetAllByIdProyecto(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor, e);
                return null;
            }
        }
    }
}
