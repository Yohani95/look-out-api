using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoFacturaController : BaseController<DocumentosFactura>
    {
        private readonly IDocumentoFacturaService _service;
        public DocumentoFacturaController(IDocumentoFacturaService service) : base(service)
        {
            _service = service;
        }
        [HttpPost("AddDocumento/{fecha}/{idFacturaPeriodo}")]
        public async Task<ActionResult<DocumentosFactura>> AddDocumento(DocumentosFactura entity, DateTime fecha, int idFacturaPeriodo)
        {
            Log.Information("[AddDocumento] Solicitud agregar documento factura");
            var data = await _service.AddDocumento(entity, fecha, idFacturaPeriodo);
            return Ok(data);
        }
        [HttpPost("AddDocumentoAnulado/{idFacturaPeriodo}")]
        public async Task<ActionResult<DocumentosFactura>> AddDocumentoAnulado(DocumentosFactura entity, int idFacturaPeriodo)
        {
            Log.Information("[AddDocumento] Solicitud agregar documento  factura anulado");
            var data = await _service.AddDocumentoAnulado(entity, idFacturaPeriodo);
            return Ok(data);
        }
        protected override int GetEntityId(DocumentosFactura entity)
        {
            throw new NotImplementedException();
        }
    }
}
