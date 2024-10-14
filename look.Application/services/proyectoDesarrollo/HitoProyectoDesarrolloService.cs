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
    internal class HitoProyectoDesarrolloService : Service<HitoProyectoDesarrollo>, IHitoProyectoDesarrolloService
    {
        private readonly IHitoProyectoDesarrolloRepository _hitoProyectoDesarrolloRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger = Logger.GetLogger();

        public HitoProyectoDesarrolloService(IHitoProyectoDesarrolloRepository repository, IUnitOfWork unitOfWork) : base(repository)
        {
            _hitoProyectoDesarrolloRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HitoProyectoDesarrollo>> GetAllByIdProyecto(int id)
        {
            try
            {
                return await _hitoProyectoDesarrolloRepository.GetAllByIdProyecto(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor, e);
                return null;
            }
        }
    }
}
