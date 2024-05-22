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
        [HttpPut("updateBag/{id}")]
        public async Task<IActionResult> UpdateBag(HorasUtilizadas horasUtilizadas,int id)
        {
            await _horasUtilizadasService.UpdateBag(horasUtilizadas,id);
            return Ok();
        }
        [HttpPost("createBag")]
        public async Task<ActionResult<HorasUtilizadas>> CreateBag(HorasUtilizadas horasUtilizadas)
        {
            var horas=await _horasUtilizadasService.CreateBag(horasUtilizadas);
            return Ok(horas);
        }
        [HttpPut("updateOnDemand/{id}")]
        public async Task<IActionResult> updateOnDemand(HorasUtilizadas horasUtilizadas, int id)
        {
            await _horasUtilizadasService.UpdateOnDemand(horasUtilizadas, id);
            return Ok();
        }
        [HttpPost("createOnDemand")]
        public async Task<ActionResult<HorasUtilizadas>> createOnDemand(HorasUtilizadas horasUtilizadas)
        {
            var horas = await _horasUtilizadasService.CreateOnDemand(horasUtilizadas);
            return Ok(horas);
        }
        protected override int GetEntityId(HorasUtilizadas entity)
        {
            return entity.Id;
        }
    }
}
