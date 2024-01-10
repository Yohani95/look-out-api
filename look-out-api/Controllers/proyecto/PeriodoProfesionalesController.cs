using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoProfesionalesController : BaseController<PeriodoProfesionales>
    {
        private readonly IPeriodoProfesionalesService _periodoProfesionalesService;

        public PeriodoProfesionalesController(IPeriodoProfesionalesService service) : base(service)
        {
            _periodoProfesionalesService = service;
        }
        [HttpGet("GetAllEntitiesByIdPeriod/{id}")]
        public async Task<ActionResult<PeriodoProfesionales>> GetAllEntitiesByIdPeriod(int id)
        {
            Log.Information("[GetAllEntitiesByIdPeriod] Solicitud getall profecionales periodos");
            var data=await _periodoProfesionalesService.GetAllEntitiesByIdPeriod(id);
            return Ok(data);
        }
        protected override int GetEntityId(PeriodoProfesionales entity)
        {
            return entity.Id;
        }
    }
}
