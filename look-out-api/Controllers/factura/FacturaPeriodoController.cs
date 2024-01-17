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
        protected override int GetEntityId(FacturaPeriodo entity)
        {
            return entity.Id;
        }
    }
}
