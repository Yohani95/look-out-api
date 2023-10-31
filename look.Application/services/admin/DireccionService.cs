using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.world;
using look.domain.interfaces.admin;
using look.domain.interfaces.cuentas;
using look.domain.interfaces.unitOfWork;
using Serilog;

namespace look.Application.services.admin;

public class DireccionService: Service<Direccion>, IDireccionService
{
    private readonly IDireccionRepository _direccionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientePersonaRepository _clientePersonaRepository;
    private readonly ILogger _logger = Logger.GetLogger();
    
    
    public DireccionService(IDireccionRepository repository,IUnitOfWork unitOfWork,IClientePersonaRepository clientePersonaRepository) : base(repository)
    {
        _direccionRepository = repository;
        _unitOfWork = unitOfWork;
        _clientePersonaRepository = clientePersonaRepository;
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
                Message = "El Direccion Creado con éxito"
            };

        }
        catch (Exception ex)
        {
            _logger.Error("Error al crear Dirrecion para la persona ID:" + direccion.PerId);
            await _unitOfWork.RollbackAsync();    
            return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
        }
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

    public async Task<ServiceResult> Edit(Direccion direccion, int id)
    {
        try
            {
                _logger.Information("Editando Email para la persona ID:" + direccion.PerId);
                if (direccion == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "La direccion proporcionada es nula." };
                }

                await _unitOfWork.BeginTransactionAsync();

                var existingDireccion = await _direccionRepository.GetByIdAsync(id); 

                if (existingDireccion == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encontró la direccion que intentas editar." };
                }
                var clientId = await _clientePersonaRepository.GetClientePersonaDTOById((int)direccion.PerId);
                if (clientId.CliId == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encuentra un cliente asociado." };
                }

                existingDireccion.DirCalle = direccion.DirCalle; 
                existingDireccion.PerId= direccion.PerId;
                existingDireccion.CliId = clientId.CliId;
                existingDireccion.DirBlock = direccion.DirBlock;
                existingDireccion.DirCalle=direccion.DirCalle;
                existingDireccion.DirNumero=direccion.DirNumero;
                existingDireccion.ComId=direccion.ComId;
                existingDireccion.TdiId=direccion.TdiId;

                await _direccionRepository.UpdateAsync(existingDireccion);
                await _unitOfWork.CommitAsync();

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "La direccion se editó con éxito"
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Error al editar la direccion para la persona ID:" + direccion.PerId);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
    }

    public async Task<List<Direccion>> GetbyIdPersona(int id)
    {
        try
        {
            return await _direccionRepository.ListCompleteByIdPersona(id);
            
        }
        catch (Exception ex)
        {
            _logger.Error(Message.ErrorServidor + ex.Message);
            return null;
        }
    }
}