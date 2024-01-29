using look.Application.interfaces.factura;
using look.domain.entities.Common;
using look.domain.entities.factura;
using look.domain.interfaces;
using look.domain.interfaces.factura;
using look.domain.interfaces.unitOfWork;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.factura
{
    public class DocumentoFacturaService : Service<DocumentosFactura>, IDocumentoFacturaService
    {
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentosFacturaRepository _repository;
        private readonly IFacturaPeriodoRepository _repositoryFacturaPeriodo;
        public DocumentoFacturaService(IDocumentosFacturaRepository repository,IUnitOfWork unitOfWork, IFacturaPeriodoRepository repositoryFacturaPeriodo) : base(repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _repositoryFacturaPeriodo = repositoryFacturaPeriodo;
        }

        public async Task<DocumentosFactura> AddDocumento(DocumentosFactura entity,DateTime fecha, int idFacturaPeriodo)
        {
            try
            {
                _logger.Information("Agregando Documento de factura", "DocumentoFacturaService, AddDocumento");
                await _unitOfWork.BeginTransactionAsync();
                if (entity.ContenidoDocumento == null)
                {
                    _logger.Error(Message.ErrorServidor, "El archivo no puede ser nulo.");
                    throw new Exception("El archivo no puede ser nulo.");
                }
                entity.IdFactura= idFacturaPeriodo;
                var factura=await _repositoryFacturaPeriodo.GetByIdAsync(idFacturaPeriodo);
                factura.IdEstado=EstadoFacturaPeriodo.ConstantesEstadoFactura.FACTURADA;
                factura.FechaVencimiento=fecha;
                await _repositoryFacturaPeriodo.UpdateAsync(factura);
                var nuevoDocumento=await _repository.AddAsync(entity);

                // Guardar los cambios en el contexto de base de datos
                await _unitOfWork.CommitAsync();

                // Devolver la entidad creada
                return nuevoDocumento;

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor, ex.Message);
                throw;
            }
        }
    }
}
