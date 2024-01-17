using look.Application.interfaces.factura;
using look.domain.entities.Common;
using look.domain.entities.factura;
using look.domain.interfaces;
using look.domain.interfaces.factura;
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
        public FacturaPeriodoService(IFacturaPeriodoRepository repository) : base(repository)
        {
            _repository = repository;
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
