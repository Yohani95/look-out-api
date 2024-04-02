using look.Application.interfaces.soporte;
using look.Application.services.Common;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using look.domain.interfaces.unitOfWork;
using Microsoft.AspNetCore.Http;
using Serilog;


namespace look.Application.services.soporte
{
    public class SoporteService : Service<Soporte>, ISoporteService
    {
        private readonly ISoporteRepository _repository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        public SoporteService(ISoporteRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResult> createAsync(List<IFormFile> files, Soporte soporte)
        {
            List<Documento> documentos = new List<Documento>();
            try
            {
                _logger.Information("Crear Soporte");
                await _unitOfWork.BeginTransactionAsync();
                if (soporte == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }
                var soporteCreated = await _repository.AddAsync(soporte);
                await _unitOfWork.CommitAsync();
                _logger.Information("Soporte creado exitosamente");
                return new ServiceResult
                {
                    MessageCode = ServiceResultMessage.Success,
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                documentos.ForEach(documento => { FileServices.DeleteFile(documento.DocUrl); });

                _logger.Error("Error al crear el Soporte: " + ex.Message);
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
            }
        }

        public async Task<IEnumerable<Soporte>> GetAllEntities()
        {
            try
            {
                _logger.Information("Actualizar Soporte");
                return await _repository.GetAllEntities();
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                return null;
            }
        }

        public async Task<Soporte> GetAllEntitiesById(int id)
        {
            try
            {
                _logger.Information("Obtener Soporte ID :" + id); 
                return await _repository.GetAllEntitiesById(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                return null;
            }
        }
    }
}
