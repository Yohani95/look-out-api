using look.Application.interfaces.admin;
using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.admin;
using look.domain.interfaces.unitOfWork;
using Microsoft.SharePoint.News.DataModel;
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
            await _unitOfWork.BeginTransactionAsync();
            var persona = await _personaRepository.AddAsync(profesional.Persona);
            profesional.Participante.PerId = persona.Id;
            await _proyectoParticipanteRepository.AddAsync(profesional.Participante);

            _logger.Information(Message.PeticionOk);
            await _unitOfWork.CommitAsync();
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
            _logger.Information(Message.ErrorServidor + ex.Message);
            return new ServiceResult
            {
                IsSuccess = false,
                Message = Message.ErrorServidor+ex.Message,
                MessageCode = ServiceResultMessage.InternalServerError
            };
        }
    }

    public async Task<ServiceResult> deletedAsync(string rut)
    {
        try
        {
            _logger.Information(Message.GetParticipanteById);
            await _unitOfWork.BeginTransactionAsync();
            var personas= await _personaRepository.GetAllAsync();
            var persona = personas.FirstOrDefault(p => p.PerIdNacional == rut);
            if (persona == null)
            {
                return new ServiceResult
                {
                    MessageCode = ServiceResultMessage.InvalidInput,
                    IsSuccess = false,
                    Message = Message.EntidadNull,
                };
            }
            var profecionales=await _proyectoParticipanteRepository.GetAllAsync();
            var profesional = profecionales.FirstOrDefault(p => p.PerId == persona.Id);
            await _proyectoParticipanteRepository.DeleteAsync(profesional);
            await _personaRepository.DeleteAsync(persona);
            _logger.Information(Message.PeticionOk);
            await _unitOfWork.CommitAsync();
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
            _logger.Information(Message.ErrorServidor + ex.Message);
            return new ServiceResult
            {
                IsSuccess = false,
                Message = Message.ErrorServidor + ex.Message,
                MessageCode = ServiceResultMessage.InternalServerError
            };
        }
    }

    public async Task<ResponseGeneric<List<ProyectoParticipante>>> GetByIdProyecto(int id)
    {
        try
        {
            _logger.Information(Message.GetParticipanteById);
            await _unitOfWork.BeginTransactionAsync();
            var profecionales = await _proyectoParticipanteRepository.GetComplete();
            var profesional = profecionales.Where(p => p.PryId==id).ToList();
            if (profesional == null)
            {
                var service= new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.NotFound,
                    Message = Message.EntidadNull
                };
                return new ResponseGeneric<List<ProyectoParticipante>>
                {
                    serviceResult = service,
                    Data = null
                };
            }
            _logger.Information(Message.PeticionOk);
            await _unitOfWork.CommitAsync();

            var result = new ServiceResult
            {
                IsSuccess = true,
                MessageCode = ServiceResultMessage.Success,
                Message = Message.PeticionOk
            };
            return new ResponseGeneric<List<ProyectoParticipante>>
            {
                serviceResult = result,
                Data = profesional
            };
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _logger.Information(Message.ErrorServidor + ex.Message);
            var result = new ServiceResult
            {
                IsSuccess = false,
                MessageCode = ServiceResultMessage.NotFound,
                Message = "No hay datos."
            };
            return new ResponseGeneric<List<ProyectoParticipante>>
            {
                serviceResult = result,
                Data = null
            };
        }
    }

    public async Task<List<ProyectoParticipante>> ListComplete()
    {
        return await _proyectoParticipanteRepository.GetComplete();
    }
}