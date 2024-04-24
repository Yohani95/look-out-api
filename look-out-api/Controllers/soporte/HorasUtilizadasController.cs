using look.Application.interfaces;
using look.Application.interfaces.soporte;
using look.Application.services.soporte;
using look.domain.entities.soporte;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.soporte
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HorasUtilizadasController : BaseController<HorasUtilizadas>
    {
        private readonly IHorasUtilizadasService _horasUtilizadasService;
        public HorasUtilizadasController(IHorasUtilizadasService service) : base(service)
        {
            _horasUtilizadasService = service;
        }
        [HttpGet("getAllHorasByIdSoporte/{id}")]
        public async Task<ActionResult<List<HorasUtilizadas>>> getAllHorasByIdSoporte(int id)
        {
            var horas = await _horasUtilizadasService.getAllHorasByIdSoporte(id);
            return Ok(horas);
        }
        protected override int GetEntityId(HorasUtilizadas entity)
        {
            return entity.Id;
        }
    }
}
