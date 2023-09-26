using look.Application.interfaces.cuentas;
using look.Application.interfaces.proyecto;
using look.Application.interfaces.world;
using look.Application.services.Common;
using look.Application.services.cuentas;
using look.Application.services.world;
using look.domain.dto.admin;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
using look.domain.entities.world;
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
        private readonly IPropuestaService _propuestaService;
        private readonly IEstadoProyectoService _estadoProyectoService;
        private readonly ITipoServicioService _tipoServicioService;
        private readonly IMonedaService _monedaService;
        private readonly IClienteService _clienteService;
        private readonly IDocumentoService _documentoService;
               
       
        public ProyectoService(IProyectoRepository proyectoRepository, IPropuestaService propuestaService, IEstadoProyectoService estadoProyectoService, ITipoServicioService tipoServicioService, IMonedaService monedaService, IClienteService clienteService, IDocumentoService documentoService) : base(proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
            _propuestaService = propuestaService;
            _estadoProyectoService = estadoProyectoService;
            _tipoServicioService = tipoServicioService;
            _monedaService = monedaService;
            _clienteService = clienteService;
            _documentoService = documentoService;
           
        }

        public async Task<ServiceResult> createAsync( IFormFile file1, IFormFile file2,Proyecto proyecto)
        {
            try
            {
                _logger.Information("Crear proyecto");
                await _unitOfWork.BeginTransactionAsync();
                if (proyecto == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }

                var urlArchivo1 = Files.UploadFileAsync(file1, (int)proyecto.PryIdCliente);
                var urlArchivo2 = Files.UploadFileAsync(file2, (int)proyecto.PryIdCliente);
                if (urlArchivo1.Equals("") || urlArchivo2.Equals(""))
                    return new ServiceResult { IsSuccess = false, Message = Message.SinDocumentos, MessageCode = ServiceResultMessage.InvalidInput };
                //completar correctamente segun lo que se requiere con todos los campos
                await _documentoService.AddAsync(new Documento { DocExtencion = file1.ContentType, DocNombre = file1.FileName, DocUrl = urlArchivo1.ToString() });
                await _documentoService.AddAsync(new Documento { DocExtencion = file2.ContentType, DocNombre = file2.FileName, DocUrl = urlArchivo2.ToString() });


             

                var propuesta = new Propuesta
                {

                };


                await _propuestaService.AddAsync(propuesta);
                await _proyectoRepository.AddAsync(proyecto);

                await _unitOfWork.CommitAsync();
                return new ServiceResult 
                { 
                    MessageCode = ServiceResultMessage.Success,
                    IsSuccess=true,
                    Message=Message.PeticionOk 
                 };

            }
            catch (Exception ex) {
                await _unitOfWork.RollbackAsync();
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message =  $"Error interno del servidor: {ex.Message}",
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
