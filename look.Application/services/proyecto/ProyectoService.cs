using look.Application.interfaces.proyecto;
using look.Application.services.Common;
using look.domain.dto.admin;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;
using look.domain.interfaces.unitOfWork;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace look.Application.services.proyecto
{
    public class ProyectoService : Service<Proyecto>, IProyectoService
    {
        //instanciar repository si se requiere 
        private readonly IProyectoRepository _proyectoRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProspectoService _prospectoService;
        public ProyectoService(IProyectoRepository proyectoRepository, IProspectoService prospectoService) : base(proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
            _prospectoService = prospectoService;
        }

        public async Task<ServiceResult> createAsync( IFormFile file1, IFormFile file2,Proyecto proyecto)
        {
            try
            {
                _logger.Information("Crear proyecto");
                if(proyecto == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message="",
                        MessageCode=ServiceResultMessage.InvalidInput
                    };
                }
                
                var urlArchivo=Files.UploadFileAsync(file1, (int) proyecto.PryIdCliente);
                var urlArchivo2 = Files.UploadFileAsync(file2, (int)proyecto.PryIdCliente);
                if(urlArchivo.Equals("")|| urlArchivo2.Equals(""))
                    return new ServiceResult { IsSuccess = false, Message="",MessageCode=ServiceResultMessage.InvalidInput };

                _unitOfWork.BeginTransactionAsync();
                var prospecto = new Prospecto {
                
                };
                _prospectoService.AddAsync(prospecto);

                _proyectoRepository.AddAsync(proyecto);

                _unitOfWork.CommitAsync();
                return new ServiceResult { MessageCode = ServiceResultMessage.Success,IsSuccess=true,Message=Message.PeticionOk };

            }
            catch (Exception ex) {
                _unitOfWork.RollbackAsync();
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "",
                    MessageCode = ServiceResultMessage.InvalidInput
                };
            }
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
