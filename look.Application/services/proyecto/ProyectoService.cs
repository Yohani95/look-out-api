using look.Application.interfaces.proyecto;
using look.domain.dto.admin;
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

        public async Task<ResponseGeneric<int>> GetLastId()
        {
            try
            {
                var proy = await _proyectoRepository.GetAllAsync();

                if(proy == null)
                {
                    var result = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.NotFound,
                        Message = "No hay datos."
                    };
                    return new ResponseGeneric<int>
                    {
                        serviceResult = result,
                        Data = 0 
                    };
                }

                var id = proy == null ? 0 : proy.LastOrDefault().PryId;

                var resultSuccess= new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = Message.PeticionOk
                };

                return new ResponseGeneric<int>
                {
                    serviceResult = resultSuccess,
                    Data = id
                };

            }
            catch (Exception ex)
            {
                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = $"Error interno del servidor: {ex.Message}"
                };

                return new ResponseGeneric<int>
                {
                    serviceResult = errorResult,
                    Data = 0
                };
            }
        }
    }
}
