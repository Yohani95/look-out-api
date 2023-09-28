using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TarifarioConvenioController:BaseController<TarifarioConvenio>
    {
        private readonly ITarifarioConvenioService _tarifarioConvenioService;

        public TarifarioConvenioController(ITarifarioConvenioService service) : base(service)
        {
            _tarifarioConvenioService = service;
        }
        
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<TarifarioConvenio>>> GetAllWithEntities()
        {
            var proyectosParticipantes = await _tarifarioConvenioService.ListComplete();

            if (proyectosParticipantes == null)
            {
                return NotFound();
            }

            return proyectosParticipantes;
        }

        protected override int GetEntityId(TarifarioConvenio entity)
        {
            return entity.TcId;
        }
    }
}
