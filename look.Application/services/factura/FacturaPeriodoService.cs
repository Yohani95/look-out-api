using look.Application.interfaces.factura;
using look.domain.entities.Common;
using look.domain.entities.factura;
using look.domain.interfaces;
using look.domain.interfaces.factura;
using look.domain.interfaces.proyecto;
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
        public FacturaPeriodoService(IFacturaPeriodoRepository repository,IUnitOfWork unitOfWork, IPeriodoProyectoRepository periodoProyectoRepository) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _periodoProyectoRepository = periodoProyectoRepository;
        }

        public async Task<bool> ChangeEstado(int idPeriodo, int estado)
        {
            try
            {
                _logger.Information("Solicitando factura, id Periodo: " + idPeriodo);
                await _unitOfWork.BeginTransactionAsync();
                await _repository.ChangeEstado(idPeriodo, estado);
                var periodo =await _periodoProyectoRepository.GetByIdAsync(idPeriodo);
                periodo.estado = 1;
                await _periodoProyectoRepository.UpdateAsync(periodo);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor + " :" + e.Message);
                return false;
            }
        }

        public Task<List<FacturaPeriodo>> GetAllByPreSolicitada()
        {
            try
            {
                return _repository.GetAllByPreSolicitada();
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + " :" + e.Message);
                return null;
            }
        }

        public Task<List<FacturaPeriodo>> GetAllEntitiesByIdPeriod(int id)
        {
            try
            {
                return _repository.GetAllByIdPeriodo(id);
            }
            catch (Exception e) { 
                _logger.Error(Message.ErrorServidor+" :"+e.Message);
                return null ;
            }
        }
    }
}
