using look.Application.interfaces.admin;
using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.interfaces;
using look.domain.interfaces.admin;
using look.domain.interfaces.unitOfWork;
using Serilog;

namespace look.Application.services.admin;

public class ProyectoParticipanteService: Service<ProyectoParticipante>, IProyectoParticipanteService
{
    
    private readonly IProyectoParticipanteRepository _proyectoParticipanteRepository;
    private readonly IPersonaRepository _personaRepository;
    private readonly ILogger _logger = Logger.GetLogger();
    private readonly IUnitOfWork _unitOfWork;

    public ProyectoParticipanteService(IProyectoParticipanteRepository repository,IUnitOfWork unitOfWork, IPersonaRepository personaRepository) : base(repository)
    {
        _proyectoParticipanteRepository = repository;
        _unitOfWork = unitOfWork;
        _personaRepository = personaRepository;
    }

    public async Task<ServiceResult> CreateDTOAsync(ProfesionalesDTO profesional)
    {
        try
        {
            _logger.Information(Message.ParticipanteCrear);

            if(profesional == null)
            {
                return new ServiceResult
                {
                    MessageCode = ServiceResultMessage.InvalidInput,
                    IsSuccess = false,
                    Message = Message.EntidadNull,
                };
            }
            _unitOfWork.BeginTransactionAsync();

            var persona = await _personaRepository.AddAsync(profesional.Persona);
            profesional.Participante.PerId = persona.Id;
            await _proyectoParticipanteRepository.AddAsync(profesional.Participante);

            _logger.Information(Message.PeticionOk);
            _unitOfWork.CommitAsync();
            return new ServiceResult
            {
                MessageCode = ServiceResultMessage.Success,
                IsSuccess = true,
                Message = Message.PeticionOk,
            };
        }
        catch (Exception ex)
        {
            _unitOfWork.RollbackAsync();
            _logger.Information(Message.ErrorServidor + ex.Message);
            return new ServiceResult
            {
                IsSuccess = false,
                Message = Message.ErrorServidor+ex.Message,
                MessageCode = ServiceResultMessage.InternalServerError
            };
        }
    }

    public async Task<List<ProyectoParticipante>> ListComplete()
    {
        return await _proyectoParticipanteRepository.GetComplete();
    }
}