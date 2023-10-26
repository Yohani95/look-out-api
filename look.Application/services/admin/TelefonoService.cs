using look.Application.interfaces.admin;
using look.Application.interfaces.cuentas;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.interfaces.admin;
using look.domain.interfaces.cuentas;
using look.domain.interfaces.unitOfWork;
using Serilog;

namespace look.Application.services.admin
{
    public class TelefonoService: Service<Telefono>, ITelefonoService
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientePersonaRepository _clientePersonaRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        
        public TelefonoService(ITelefonoRepository repository,IUnitOfWork unitOfWork,IClientePersonaRepository clientePersonaRepository) : base(repository)
        {
            _telefonoRepository = repository;
            _unitOfWork = unitOfWork;
            _clientePersonaRepository = clientePersonaRepository;
        }

        public async Task<List<Telefono>> ListComplete()
        {
            return await _telefonoRepository.GetComplete();
        }

        public async Task<ServiceResult> Create(Telefono telefono)
        {
            try
            {
                _logger.Information("Creando Telefono para la persona ID:" + telefono.perId);
                if (telefono == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El telefono proporcionado es nulo." };
                }
                await _unitOfWork.BeginTransactionAsync();
                var clientId = await _clientePersonaRepository.GetClientePersonaDTOById((int)telefono.perId);
                if (clientId.CliId==null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encuentra un cliente asociado." };
                }
                IEnumerable<Telefono> telefonos = await _telefonoRepository.GetAllAsync();
                bool telefonoExists = telefonos.Any(e => e.telNumero == telefono.telNumero);
                if (telefonoExists)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.Conflict, Message = "El telefono ya existe en la base de datos." };
                }

                telefono.cliId = clientId.CliId;
                await _telefonoRepository.AddAsync(telefono);
                await _unitOfWork.CommitAsync();
                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "El telefono Creado con éxito"
                };

            }
            catch (Exception ex)
            {
                _logger.Error("Error al crear telefono para la persona ID:" + telefono.perId);
                await _unitOfWork.RollbackAsync();    
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }

        public async Task<ServiceResult> Edit(Telefono telefono, int id)
        {
            try
            {
                _logger.Information("Editando telefono para la persona ID:" + telefono.perId);
                if (telefono == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El telefono proporcionado es nulo." };
                }

                await _unitOfWork.BeginTransactionAsync();

                var existingtTelefono = await _telefonoRepository.GetByIdAsync(id); 

                if (existingtTelefono == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encontró el telefono que intentas editar." };
                }
                var clientId = await _clientePersonaRepository.GetClientePersonaDTOById((int)telefono.perId);
                if (clientId.CliId == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encuentra un cliente asociado." };
                }

                existingtTelefono.telNumero = telefono.telNumero; 
                existingtTelefono.perId= telefono.perId;
                existingtTelefono.cliId = clientId.CliId;
                existingtTelefono.telVigente = telefono.telVigente;
                existingtTelefono.tteId=telefono.tteId;

                await _telefonoRepository.UpdateAsync(existingtTelefono);
                await _unitOfWork.CommitAsync();

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "El telefono se editó con éxito"
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Error al editar el telefono para la persona ID:" + telefono.perId);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }
        
        public async Task<List<Telefono>> ListCompleteById(int id)
        {
            return await _telefonoRepository.ListCompleteById(id);
        }
    }
}

