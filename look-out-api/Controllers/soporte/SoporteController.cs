using look.Application.interfaces;
using look.Application.interfaces.soporte;
using look.domain.entities.soporte;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.soporte
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SoporteController : BaseController<Soporte>
    {
        private readonly ISoporteService _soporteService;
        public SoporteController(ISoporteService service) : base(service)
        {
            _soporteService = service;
        }
        [HttpGet("GetAllEntities")]
        public async Task<ActionResult<List<Soporte>>> GetAllEntities()
        {
            var soportes = await _soporteService.GetAllEntities();
            return Ok(soportes);
        }
        [HttpGet("GetAllEntitiesById/{id}")]
        public async Task<ActionResult<List<Soporte>>> GetAllEntitiesById(int id)
        {
            var soporte = await _soporteService.GetAllEntitiesById(id);
            if (soporte == null)
            {
                return NotFound();
            }
            return Ok(soporte);
        }
        protected override int GetEntityId(Soporte entity)
        {
            return entity.PryId; 
        }

    }
}
