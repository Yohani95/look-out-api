using look.Application.interfaces.factura;
using look.domain.entities.Common;
using look.domain.entities.factura;
using look.domain.entities.soporte;
using look.domain.interfaces;
using look.domain.interfaces.factura;
using look.domain.interfaces.proyecto;
using look.domain.interfaces.soporte;
using look.domain.interfaces.unitOfWork;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.factura
{
    public class FacturaPeriodoService : Service<FacturaPeriodo>, IFacturaPeriodoService
    {
        private readonly IFacturaPeriodoRepository _repository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPeriodoProyectoRepository _periodoProyectoRepository;
        private readonly IHorasUtilizadasRepository _horasUtilizadasRepository;
        public FacturaPeriodoService(IFacturaPeriodoRepository repository, IUnitOfWork unitOfWork, IPeriodoProyectoRepository periodoProyectoRepository, IHorasUtilizadasRepository horasUtilizadasRepository) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _periodoProyectoRepository = periodoProyectoRepository;
            _horasUtilizadasRepository = horasUtilizadasRepository;
        }

        public async Task<bool> ChangeEstado(int idPeriodo, int estado)
        {
            try
            {
                _logger.Information("Solicitando factura, id Periodo: " + idPeriodo);
                await _unitOfWork.BeginTransactionAsync();
                await _repository.ChangeEstado(idPeriodo, estado);
                var periodo = await _periodoProyectoRepository.GetByIdAsync(idPeriodo);
                if (periodo != null)
                {
                    periodo.estado = 1;
                    await _periodoProyectoRepository.UpdateAsync(periodo);
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor + e.Message);
                return false;
            }
        }

        public async Task<bool> ChangeEstadoHoras(int idHoras, int estado)
        {
            try
            {
                _logger.Information("Solicitando factura, id Periodo: " + idHoras);
                await _unitOfWork.BeginTransactionAsync();
                await _repository.ChangeEstadoHoras(idHoras, estado);
                var horasPeriodo = await _horasUtilizadasRepository.GetByIdAsync(idHoras);
                if (horasPeriodo != null)
                {
                    horasPeriodo.Estado = true;
                    await _horasUtilizadasRepository.UpdateAsync(horasPeriodo);
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor + e.Message);
                return false;
            }
        }

        public async Task<List<FacturaPeriodo>> GetAllByIdHoras(int id)
        {
            try
            {
                return await _repository.GetAllByIdHoras(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                return null;
            }
        }

        public async Task<List<FacturaPeriodo>> GetAllByPreSolicitada()
        {
            try
            {
                return await _repository.GetAllByPreSolicitada();
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                throw;
            }
        }

        public async Task<List<FacturaPeriodo>> GetAllEntitiesByIdPeriod(int id)
        {
            try
            {
                return await _repository.GetAllByIdPeriodo(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                return null;
            }
        }

        public async Task<FacturaPeriodo> UpdateFactura(FacturaPeriodo entity, int idFacturaPeriodo)
        {
            try
            {
                var factura = await _repository.GetByIdAsync(idFacturaPeriodo);
                factura.IdEstado = entity.IdEstado;
                await _repository.UpdateAsync(factura);
                return factura;
            }
            catch (Exception)
            {
                _logger.Error("[UpdateFactura]", Message.ErrorServidor, "idFacturaPeriodo: " + idFacturaPeriodo);
                return null;
            }
        }
        public new async Task UpdateAsync(FacturaPeriodo factura)
        {
            try
            {
                await _repository.UpdateAsync(factura);
            }
            catch (Exception e)
            {

                _logger.Error("[UpdateFactura]", Message.ErrorServidor, e.Message);
            }
        }

        public async Task<List<FacturaPeriodo>> GetAllByIdSoporte(int id)
        {
            try
            {
                return await _repository.GetAllByIdSoporte(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                return null;
            }
        }

        public async Task<bool> ChangeEstadoSoporte(int idSoporte, int estado)
        {
            try
            {
                _logger.Information("Solicitando factura, id Periodo: " + idSoporte);
                await _unitOfWork.BeginTransactionAsync();
                await _repository.ChangeEstadoSoporte(idSoporte, estado);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor + e.Message);
                return false;
            }
        }

        public async Task<bool> ChangeEstadoByLicencia(int idlicencia, int estado)
        {
            try
            {
                _logger.Information("Solicitando factura, id Periodo: " + idlicencia);
                await _unitOfWork.BeginTransactionAsync();
                await _repository.ChangeEstadoByLicencia(idlicencia, estado);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor + e.Message);
                return false;
            }
        }

        public async Task<List<FacturaPeriodo>> GetAllEntitiesByIdLicense(int id)
        {
            try
            {
                return await _repository.GetAllEntitiesByIdLicense(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                return null;
            }
        }
    }
}
