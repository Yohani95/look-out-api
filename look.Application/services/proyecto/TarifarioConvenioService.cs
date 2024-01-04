using look.Application.interfaces.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using Serilog;

namespace look.Application.services.proyecto
{
    public class TarifarioConvenioService: Service<TarifarioConvenio>, ITarifarioConvenioService
    {
    
        private readonly ITarifarioConvenioRepository _tarifarioConvenioService;
        private readonly ILogger _logger = Logger.GetLogger();

        public TarifarioConvenioService(ITarifarioConvenioRepository repository) : base(repository)
        {
            _tarifarioConvenioService = repository;
        }

        public async Task<ResponseGeneric<TarifarioConvenio>> GetByIdEntities(int id)
        {
            try
            {
                var tarifario = await _tarifarioConvenioService.GetComplete();
                if (id <= 0)
                {
                    var result = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.NotFound,
                        Message = Message.IdNull
                    };
                    return new ResponseGeneric<TarifarioConvenio>
                    {
                        serviceResult = result,
                        Data = null
                    };
                }
                var resultSuccess = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = Message.PeticionOk
                };
                return new ResponseGeneric<TarifarioConvenio>
                {
                    serviceResult = resultSuccess,
                    Data = tarifario.FirstOrDefault(t => t.TcId == id)
                };
            }
            catch (Exception ex)
            {
                _logger.Information(Message.ErrorServidor + ex.Message);
                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = Message.ErrorServidor + ex.Message
                };

                return new ResponseGeneric<TarifarioConvenio>
                {
                    serviceResult = errorResult,
                    Data = null
                };
            }
        }

        public async Task<TarifarioConvenio> GetbyIdEntities(int id)
        {
            return await _tarifarioConvenioService.GetbyIdEntities(id);
        }

        public async Task<ResponseGeneric<List<TarifarioConvenio>>> GetByIdProyectoEntities(int idProyecto)
        {
            try
            {
                var tarifario = await _tarifarioConvenioService.GetComplete();
                if (idProyecto <= 0)
                {
                    var result = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.NotFound,
                        Message = Message.IdNull
                    };
                    return new  ResponseGeneric<List<TarifarioConvenio>>
                    {
                        serviceResult = result,
                        Data = null
                    };
                }
                var resultSuccess = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = Message.PeticionOk
                };
                return new ResponseGeneric<List<TarifarioConvenio>>
                {
                    serviceResult = resultSuccess,
                    Data = tarifario.Where(t => t.PRpId == idProyecto).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.Information(Message.ErrorServidor + ex.Message);
                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = Message.ErrorServidor + ex.Message
                };

                return new ResponseGeneric<List<TarifarioConvenio>>
                {
                    serviceResult = errorResult,
                    Data = null
                };
            }
        }

        public async Task<List<TarifarioConvenio>> ListComplete()
        {
            return await _tarifarioConvenioService.GetComplete();
        }
    }
}

