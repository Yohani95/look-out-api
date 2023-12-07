using look.Application.interfaces.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces.admin;
using look.domain.interfaces.proyecto;
using look.domain.interfaces.unitOfWork;
using Serilog;

namespace look.Application.services.proyecto
{
    
    public class NovedadesService: Service<Novedades>, INovedadesService
    {
        private readonly INovedadesRepository _novedadesRepository;
        
        private readonly IProyectoParticipanteRepository _proyectoParticipanteRepository;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger = Logger.GetLogger();
        
        public NovedadesService(INovedadesRepository repository,IProyectoParticipanteRepository proyectoParticipanteRepository,IUnitOfWork unitOfWork) : base(repository)
        {
            _novedadesRepository = repository;
            _proyectoParticipanteRepository = proyectoParticipanteRepository;
            _unitOfWork= unitOfWork;
        }

        public async Task<List<Novedades>> ListComplete()
        {
            return await _novedadesRepository.GetComplete();
        }

        public async Task<ServiceResult> updateNovedad(Novedades novedad)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var personas = await _proyectoParticipanteRepository.GetAllAsync();
                var novedades = new Novedades();
                novedades.id = novedad.id;
                novedades.idPersona =  novedad.idPersona;
                novedades.idProyecto =novedad.idProyecto;
                novedades.fechaInicio =novedad.fechaInicio;
                novedades.fechaHasta =novedad.fechaHasta;
                novedades.observaciones =  novedad.observaciones;
                novedades.IdPerfil =  novedad.IdPerfil;
                novedades.IdTipoNovedad =  novedad.IdTipoNovedad;
                await _novedadesRepository.UpdateAsync(novedades);

                var participante = personas.FirstOrDefault(p => p.PryId == novedad.idProyecto && p.PerId == novedad.idPersona);
                
                if (participante != null && novedad.IdPerfil != participante.PrfId)
                {
                        participante.PrfId = (int)novedad.IdPerfil;
                        await _proyectoParticipanteRepository.UpdateAsync(participante);
                }
                
                _logger.Information("novedades y proyecto participante actualizados correctamente");
                await _unitOfWork.CommitAsync();
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                    MessageCode = ServiceResultMessage.Success
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Error al actualizar novedades y proyecto participante: " + ex.Message);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
            }
        }
        public async Task<ServiceResult> createNovedad(Novedades novedad)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _novedadesRepository.AddAsync(novedad);
 #region "Cambio de rol"
                if (novedad.IdPerfil!=null || novedad.IdPerfil==0){
                    var personas = await _proyectoParticipanteRepository.GetAllAsync();
                    var participante = personas.FirstOrDefault(p => p.PryId == novedad.idProyecto && p.PerId == novedad.idPersona);

                    if (participante != null && novedad.IdPerfil != participante.PrfId)
                    {
                        participante.PrfId = (int)novedad.IdPerfil;
                        await _proyectoParticipanteRepository.UpdateAsync(participante);
                    }
                }
#endregion
                _logger.Information("novedades");
                await _unitOfWork.CommitAsync();
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                    MessageCode = ServiceResultMessage.Success
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Error al actualizar novedades " + ex.Message);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
            }
        }
    }
}
