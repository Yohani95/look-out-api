using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaPeriodoController : BaseController<FacturaPeriodo>
    {
        private readonly IFacturaPeriodoService _service;
        public FacturaPeriodoController(IFacturaPeriodoService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("GetAllEntitiesByIdPeriod/{id}")]
        public async Task<ActionResult<FacturaPeriodo>> GetAllEntitiesByIdPeriod(int id)
        {
            Log.Information("[GetAllEntitiesByIdPeriod] Solicitud getall facturas periodos IDPERIODO : " +id);
            var data=await _service.GetAllEntitiesByIdPeriod(id);
            return Ok(data);
        }
        [HttpGet("GetAllByPreSolicitada")]
        public async Task<ActionResult<FacturaPeriodo>> GetAllByPreSolicitada()
        {
            Log.Information("[GetAllEntitiesByIdPeriod] Solicitud getall facturas diferente a pendiente " );
            var data = await _service.GetAllByPreSolicitada();
            return Ok(data);
        }
        [HttpGet("ChangeEstado/{idPeriodo}/{estado}")]
        public async Task<IActionResult> ChangeEstado(int idPeriodo, int estado)
        {
            Log.Information("[GetAllEntitiesByIdPeriod] Cambiar estado idPeriodo: "+idPeriodo+", Estado ID:"+estado);
            var data = await _service.ChangeEstado(idPeriodo,estado);
            return Ok(data);
        }
        protected override int GetEntityId(FacturaPeriodo entity)
        {
            return entity.Id;
        }
    }
}
