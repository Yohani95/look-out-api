using look.Application.interfaces.admin;
using look.domain.entities.Common;
using look.domain.entities.world;
using look.domain.interfaces.admin;
using look.domain.interfaces.cuentas;
using look.domain.interfaces.unitOfWork;
using Serilog;

namespace look.Application.services.admin;

public class DireccionService: Service<Direccion>, IDireccionService
{
    private IDireccionRepository _direccionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientePersonaRepository _clientePersonaRepository;
    private readonly ILogger _logger = Logger.GetLogger();
    
    
    public DireccionService(IDireccionRepository repository,IUnitOfWork unitOfWork,IClientePersonaRepository clientePersonaRepository) : base(repository)
    {
        _direccionRepository = repository;
        _unitOfWork = unitOfWork;
        _clientePersonaRepository = clientePersonaRepository;
    }
    
    public async Task<List<Direccion>> ListComplete()
    {
        try
        {
            return await _direccionRepository.GetComplete();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<ServiceResult> Create(Direccion direccion)
    {
        try
        {
            _logger.Information("Creando Direccion para la persona ID:" + direccion.PerId);
            if (direccion == null)
            {
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "La Direccion proporcionada es nulo." };
            }
            await _unitOfWork.BeginTransactionAsync();
            var clientId = await _clientePersonaRepository.GetClientePersonaDTOById((int)direccion.PerId);
            if (clientId.CliId==null)
            {
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encuentra un Direccion asociado." };
            }
            IEnumerable<Direccion> direcciones = await _direccionRepository.GetAllAsync();
            bool direccionExists = direcciones.Any(e => e.DirCalle == direccion.DirCalle);
            if (direccionExists)
            {
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.Conflict, Message = "La Direccion ya existe en la base de datos." };
            }

            direccion.CliId = clientId.CliId;
            await _direccionRepository.AddAsync(direccion);
            await _unitOfWork.CommitAsync();
            return new ServiceResult
            {
                IsSuccess = true,
                MessageCode = ServiceResultMessage.Success,
                Message = "El email Creado con éxito"
            };

        }
        catch (Exception ex)
        {
            _logger.Error("Error al crear Email para la persona ID:" + direccion.PerId);
            await _unitOfWork.RollbackAsync();    
            return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
        }
    }

    public Task<ServiceResult> Edit(Direccion email, int id)
    {
        return null;
        //return _direccionRepository.Edit(email, id);
    }
}