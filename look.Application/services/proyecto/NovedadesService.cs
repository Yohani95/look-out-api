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
                
                foreach (var participante in personas.Where(p => p.PryId == novedad.idProyecto).Where(p=>p.PerId==novedad.idPersona))
                {
                    if (novedad.IdPerfil != 0)
                    {
                        
                        var proyectoParticipante = new ProyectoParticipante();
                        proyectoParticipante.PpaId = participante.PpaId;
                        proyectoParticipante.PryId = participante.PryId;
                        proyectoParticipante.PerId = participante.PerId;
                        proyectoParticipante.CarId = participante.CarId;
                        proyectoParticipante.PerTarifa = participante.PerTarifa;
                        proyectoParticipante.PrfId = novedad.IdPerfil;
                        proyectoParticipante.FechaAsignacion = participante.FechaAsignacion;
                        proyectoParticipante.FechaTermino = participante.FechaTermino;
                        proyectoParticipante.estado = participante.estado;
                        
                        await _proyectoParticipanteRepository.UpdateAsync(proyectoParticipante);
                    }
                
                    
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
    }
}
