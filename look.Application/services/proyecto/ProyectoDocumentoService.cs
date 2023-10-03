using look.Application.interfaces.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using Serilog;

namespace look.Application.services.proyecto
{
    public class ProyectoDocumentoService: Service<ProyectoDocumento>, IProyectoDocumentoService
    {
    
        private readonly IProyectoDocumentoRepository _proyectoDocumentoRepository;
        private readonly ILogger _logger = Logger.GetLogger();

        public ProyectoDocumentoService(IProyectoDocumentoRepository repository) : base(repository)
        {
            _proyectoDocumentoRepository = repository;
        }

        public async Task<ProyectoDocumento> GetByIdProject(int id)
        {
            try
            {
                var proyectos=await _proyectoDocumentoRepository.GetAllAsync();
                if (proyectos != null)
                {
                    return null;
                }
                return proyectos.Where(p=>p.PryId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error("Error interno del servidor: " + ex.Message);

                return null;
            }
        }

        public async Task<List<ProyectoDocumento>> ListComplete()
        {
            return await _proyectoDocumentoRepository.GetComplete();
        }
    }
}


    
