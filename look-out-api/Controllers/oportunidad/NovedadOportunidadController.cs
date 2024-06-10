using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.Application.services.oportunidad;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadOportunidadController : BaseController<NovedadOportunidad>
    {
        private readonly INovedadOportunidadService _oportunidadService;
        public NovedadOportunidadController(INovedadOportunidadService service) : base(service)
        {
            _oportunidadService = service;
        }
        [HttpGet("getbyIdOportunidad/{id}")]
        public async Task<IActionResult> GetLastId(int id)
        {
            var result = await _oportunidadService.GetByIdOportunidad(id);
            return Ok(result);
        }

        protected override int GetEntityId(NovedadOportunidad entity)
        {
            return entity.Id;   
        }
    }
}
