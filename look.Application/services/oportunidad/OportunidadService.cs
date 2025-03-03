using look.Application.interfaces.admin;
using look.Application.interfaces.oportunidad;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.oportunidad;
using look.domain.entities.prospecto;
using look.domain.interfaces;
using look.domain.interfaces.oportunidad;
using look.domain.interfaces.unitOfWork;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.oportunidad
{
    public class OportunidadService : Service<Oportunidad>, IOportunidadService
    {
        private readonly IOportunidadRepository _repository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public OportunidadService(IOportunidadRepository repository, IUnitOfWork unitOfWork, IEmailService emailService) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        public async new Task UpdateAsync(Oportunidad oportunidad)
        {
            try
            {
                var result = await _repository.GetByIdAsync(oportunidad.Id);
                var idEstado = result.IdEstadoOportunidad;
                if (result == null)
                {
                    _logger.Warning($"No se encontró una oportunidad con el ID {oportunidad.Id}");
                    throw new KeyNotFoundException($"No se encontró una oportunidad con el ID {oportunidad.Id}");
                }

                // Actualizar los campos directamente
                result.Nombre = oportunidad.Nombre ?? result.Nombre;
                result.FechaCierre = oportunidad.FechaCierre ?? result.FechaCierre;
                result.IdEstadoOportunidad = oportunidad.IdEstadoOportunidad ?? result.IdEstadoOportunidad;
                result.IdCliente = oportunidad.IdCliente ?? result.IdCliente;
                result.IdMoneda = oportunidad.IdMoneda ?? result.IdMoneda;
                result.Monto = oportunidad.Monto ?? result.Monto;
                result.IdTipoOportunidad = oportunidad.IdTipoOportunidad ?? result.IdTipoOportunidad;
                result.IdPais = oportunidad.IdPais ?? result.IdPais;
                result.Renovable = oportunidad.Renovable ?? result.Renovable;
                result.IdLicitacion = oportunidad.IdLicitacion ?? result.IdLicitacion;
                result.FechaRenovacion = oportunidad.FechaRenovacion ?? result.FechaRenovacion;
                result.IdEmpresaPrestadora = oportunidad.IdEmpresaPrestadora ?? result.IdEmpresaPrestadora;
                result.IdAreaServicio = oportunidad.IdAreaServicio ?? result.IdAreaServicio;
                result.IdContacto = oportunidad.IdContacto ?? result.IdContacto;
                result.IdKam = oportunidad.IdKam ?? result.IdKam;
                result.Descripcion = oportunidad.Descripcion ?? result.Descripcion;
                result.IdOrigen = oportunidad.IdOrigen ?? result.IdOrigen;
                result.IdTipoLicencia = oportunidad.IdTipoLicencia ?? result.IdTipoLicencia;
                result.IdTipoCerrada = oportunidad.IdTipoCerrada ?? result.IdTipoCerrada;

                // Guardar cambios en el repositorio
                await _repository.UpdateAsync(result);
                var resultChange = await _repository.GetByIdAsync(oportunidad.Id);
                //enviar emails segun corresponda
                if (idEstado != oportunidad.IdEstadoOportunidad)
                {
                    _logger.Information("Enviando el Email");
                    if (oportunidad.IdEstadoOportunidad == EstadoOportunidad.PropuestaEnPreparacion.Id)
                    {
                        await _emailService.EnviarEmailDelevery(resultChange);
                        _logger.Information("Email enviado a Encargado Delevery");
                    }
                    else if (result.IdEstadoOportunidad == EstadoOportunidad.PropuestaEntregadaComercial.Id)
                    {
                        await _emailService.EnviarEmailKam((int)result.IdKam, resultChange);
                        _logger.Information("Email enviado a Encargado Kam");

                    }
                    else if (result.IdEstadoOportunidad == EstadoOportunidad.CerradaPerdida.Id || result.IdEstadoOportunidad == EstadoOportunidad.CerradaGanada.Id)
                    {
                        await _emailService.EnviarEmailDelevery(resultChange);
                        _logger.Information("Email enviado a Encargado Delevery");
                    }
                }
                //return result;
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor, e.Message);
                //return null;
            }
        }
    }
}
