using look.Application.interfaces.admin;
using look.Application.interfaces.cuentas;
using MailKit.Net.Smtp;
using MailKit.Security;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.interfaces.admin;
using look.domain.interfaces.cuentas;
using look.domain.interfaces.unitOfWork;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;
using Microsoft.Extensions.Options;
using look.domain.entities.oportunidad;
using System.Reflection.Metadata.Ecma335;

namespace look.Application.services.admin
{
    public class EmailService : Service<Email>, IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientePersonaRepository _clientePersonaRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly EmailSettings _emailSettings;
        public EmailService(IEmailRepository repository, IUnitOfWork unitOfWork, IClientePersonaRepository clientePersonaRepository,
           IOptions<EmailSettings> emailSettings
            ) : base(repository)
        {
            _emailRepository = repository;
            _unitOfWork = unitOfWork;
            _clientePersonaRepository = clientePersonaRepository;
            _emailSettings = emailSettings.Value;
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
                if (clientId.CliId == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "No se encuentra un cliente asociado." };
                }
                IEnumerable<Email> emails = await _emailRepository.GetAllAsync();
                bool emailExists = emails.Any(e => e.EmaEmail == email.EmaEmail);
                if (emailExists)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.Conflict, Message = "El email ya existe en la base de datos." };
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
        public async Task<ServiceResult> Edit(Email email, int id)
        {
            try
            {
                _logger.Information("Editando Email para la persona ID:" + email.PerId);
                if (email == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El email proporcionado es nulo." };
                }

                await _unitOfWork.BeginTransactionAsync();

                var existingEmail = await _emailRepository.GetByIdAsync(id);

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
                existingEmail.PerId = email.PerId;
                existingEmail.CliId = clientId.CliId;
                existingEmail.EmaVigente = email.EmaVigente;
                existingEmail.TemId = email.TemId;

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
                _logger.Information("Obteniendo Emails");
                return await _emailRepository.GetComplete();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Email>> ListCompleteById(int id)
        {
            try
            {
                _logger.Information("Obteniendo Emails de Idpersona: " + id);
                return await _emailRepository.ListCompleteByIdPersona(id);
            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor + ex.Message);
                return null;
            }
        }

        public async Task SendEmailAsync(string toName, string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Cc.Add(new MailboxAddress("Copia Responsable de Delivery", _emailSettings.CopiaFija));
            message.Subject = subject;

            // Crear el cuerpo del correo en HTML
            message.Body = new TextPart("html")
            {
                Text = $@"
            <html>
            <body>
                <p>Hola {toName},</p>
                <p>{body}</p>
                <p>Saludos,<br>{_emailSettings.SenderName}.</p>
                <footer style='border-top: 1px solid #cccccc; padding-top: 10px; margin-top: 10px;'>
                    <p>Este es un mensaje automático, por favor no responder a este correo.</p>
                    <p>Si tiene alguna consulta, por favor contacte con Soporte.</p>
                    <p>Gracias!</p>
                </body>
            </html>"
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new InvalidOperationException("Error al enviar el correo electrónico", ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        public async Task EnviarEmailDelevery(Oportunidad oportunidad)
        {
            // Crear el cuerpo del mensaje basado en los datos de la oportunidad
            var body = $@"
            <p><strong>Cliente:</strong> {oportunidad.Cliente.CliNombre}</p>
            <p><strong>Nombre Propuesta:</strong> {oportunidad.Nombre}</p>
            <p><strong>Tipo Negocio:</strong> {oportunidad.TipoOportunidad.Nombre}</p>
            <p><strong>Moneda:</strong> {oportunidad.Moneda.MonNombre}</p>
            <p><strong>Monto:</strong> {oportunidad.Monto}</p>
            <p><strong>Estado:</strong> {oportunidad.EstadoOportunidad?.Nombre}</p>";


            await SendEmailAsync("Responsable de Delivery", _emailSettings.ResponsableDelevery, "Cambio de Estado Oportunidad", body);
        }

        public async Task EnviarEmailKam(int id, Oportunidad oportunidad)
        {
            // Crear el cuerpo del mensaje basado en los datos de la oportunidad
            var email = await GetEmailByPersona(id);
            if (email == null)
            {
                _logger.Warning("No se encontró un email para el ID de la persona: {Id}", id);
                return;
            }
            var body = "<p>Tienes una propuesta en estado " +
                       "<strong>Propuesta Entregada a Comercial</strong> " +
                       "para ser gestionada.</p>" +
                       "<p><strong>Propuesta ID: </strong>" + oportunidad.Id + "</p>" +
                       "<p><strong>Nombre: </strong>" + oportunidad.Nombre + ".</p>" +
                       "<p><strong>Cliente: </strong> " + oportunidad.Cliente?.CliNombre + "</p>" +
                       "<p><strong>Tipo Negocio :</strong> " + oportunidad.TipoOportunidad?.Nombre + "</p>" +
                       "<p><strong>Moneda: </strong> " + oportunidad.Moneda?.MonNombre + "</p>" +
                       "<p><strong>Monto: </strong> $" + oportunidad.Monto + "</p>" +
                       "<p><strong>Estado: </strong> $" + oportunidad.EstadoOportunidad?.Nombre + "</p>" +
                       "<p> La propuesta está lista para ser enviada a cliente.</p>";

            await SendEmailAsync(
                oportunidad.PersonaKam.PerNombres ?? "KAM",
                email.EmaEmail,
                "Cambio de Estado Oportunidad",
                body
            );
            _logger.Information("Correo enviado exitosamente a {Email}", email.EmaEmail);
        }

        public async Task<Email> GetEmailByPersona(int id)
        {
            return await _emailRepository.GetyEmailByPersona(id);
        }
    }
}