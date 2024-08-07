using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.Application.services.factura;
using look.domain.entities.Common;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaAdaptacionController : BaseController<FacturaAdaptacion>
    {
        private readonly IFacturaAdaptacionService _service;
        public FacturaAdaptacionController(IFacturaAdaptacionService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("GetAllEntitiesByIdLicense/{id}")]
        public async Task<ActionResult<FacturaAdaptacion>> GetAllEntitiesByIdLicense(int id)
        {
            var result = await _service.GetAllEntitiesByIdLicense(id);
            if (result == null)
            {
                return NoContent(); // Devuelve un 204 si no hay contenido
            }
            return Ok(result); // D
        }
        [HttpGet("GetAllEntitiesByIdPeriod/{id}")]
        public async Task<ActionResult<FacturaAdaptacion>> GetAllEntitiesByIdPeriod(int id)
        {
            var result = await _service.GetAllEntitiesByIdPeriod(id);
            if (result == null)
            {
                return NoContent(); // Devuelve un 204 si no hay contenido
            }
            return Ok(result);
        }
        [HttpGet("GetAllEntitiesByIdHoras/{id}")]
        public async Task<ActionResult<FacturaAdaptacion>> GetAllByIdHoras(int id)
        {
            var result = await _service.GetAllByIdHoras(id);
            if (result == null)
            {
                return NoContent(); // Devuelve un 204 si no hay contenido
            }
            return Ok(result);
        }
        [HttpGet("GetAllEntitiesByIdSoporte/{id}")]
        public async Task<ActionResult<FacturaAdaptacion>> GetAllEntitiesByIdSoporte(int id)
        {
            var result = await _service.GetAllByIdSoporte(id);
            if (result == null)
            {
                return NoContent(); // Devuelve un 204 si no hay contenido
            }
            return Ok(result);
        }
        protected override int GetEntityId(FacturaAdaptacion entity)
        {
            return entity.Id;
        }
    }
}
