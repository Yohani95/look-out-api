using look.Application.interfaces.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class ProyectoService : Service<Proyecto>, IProyectoService
    {
        //instanciar repository si se requiere 
        private readonly IProyectoRepository _proyectoRepository;

        public ProyectoService(IProyectoRepository proyectoRepository) : base(proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<int> GetLastId()
        {
            try
            {
                var proy = await _proyectoRepository.GetAllAsync();
                var id = proy == null ? 0 : proy.LastOrDefault().PryId;                               
                return id;
            }
            catch(Exception ex)
            {
                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = $"Error interno del servidor: {ex.Message}"
                };

                return 0;
            }
            
        }
    }
}
