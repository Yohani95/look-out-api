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
    public class EmailService: Service<Email>, IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientePersonaRepository _clientePersonaRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        public EmailService(IEmailRepository repository,IUnitOfWork unitOfWork,IClientePersonaRepository clientePersonaRepository) : base(repository)
        {
            _emailRepository = repository;
            _unitOfWork = unitOfWork;
            _clientePersonaRepository = clientePersonaRepository;
        }

        public async Task<ServiceResult> Create(Email email)
        {
            try
            {
                _logger.Information("Creando Email para la persona ID:" + email.PerId);
                if (email == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El email proporcionado es nulo." };
                }
                await _unitOfWork.BeginTransactionAsync();
                var clientId = await _clientePersonaRepository.GetClientePersonaDTOById((int)email.PerId);
                if (clientId.CliId==null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encuentra un cliente asociado." };
                }
                IEnumerable<Email> emails = await _emailRepository.GetAllAsync();
                bool emailExists = emails.Any(e => e.EmaEmail == email.EmaEmail);
                if (emailExists)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El email ya existe en la base de datos." };
                }

                email.CliId = clientId.CliId;
                await _emailRepository.AddAsync(email);
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
                _logger.Error("Error al crear Email para la persona ID:" + email.PerId);
                await _unitOfWork.RollbackAsync();    
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }
        public async Task<ServiceResult> Edit(Email email)
        {
            try
            {
                _logger.Information("Editando Email para la persona ID:" + email.PerId);
                if (email == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El email proporcionado es nulo." };
                }

                await _unitOfWork.BeginTransactionAsync();

                var existingEmail = await _emailRepository.GetByIdAsync(email.EmaId); 

                if (existingEmail == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encontró el email que intentas editar." };
                }
                var clientId = await _clientePersonaRepository.GetClientePersonaDTOById((int)email.PerId);
                if (clientId.CliId == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encuentra un cliente asociado." };
                }

                existingEmail.EmaEmail = email.EmaEmail; 
                existingEmail.PerId= email.PerId;
                existingEmail.CliId = clientId.CliId;
                existingEmail.EmaVigente = email.EmaVigente;
                existingEmail.TemId=email.TemId;

                await _emailRepository.UpdateAsync(existingEmail);
                await _unitOfWork.CommitAsync();

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "El email se editó con éxito"
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Error al editar el Email para la persona ID:" + email.PerId);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }



        public async Task<List<Email>> ListComplete()
        {
            try
            {
                return await _emailRepository.GetComplete();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}