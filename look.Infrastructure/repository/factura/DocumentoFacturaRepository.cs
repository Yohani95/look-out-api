using look.domain.entities.factura;
using look.domain.interfaces.factura;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.factura
{
    public class DocumentoFacturaRepository : Repository<DocumentosFactura>, IDocumentosFacturaRepository
    {
        public DocumentoFacturaRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public Task<DocumentosFactura> AddFactura(DocumentosFactura documentoFactura)
        {
            throw new NotImplementedException();
        }
    }
}
