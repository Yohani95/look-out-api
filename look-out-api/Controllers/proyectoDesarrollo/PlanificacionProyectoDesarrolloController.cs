using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.Application.services;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanificacionProyectoDesarrolloController : BaseController<PlanificacionProyectoDesarrollo>
    {
        private readonly IPlanificacionProyectoDesarrolloService _service;
        public PlanificacionProyectoDesarrolloController(IPlanificacionProyectoDesarrolloService service) : base(service)
        {
            _service = service;
        }

        protected override int GetEntityId(PlanificacionProyectoDesarrollo entity)
        {
            return entity.Id;
        }
        [HttpGet("GetAllByProyecto/{id}")]
        [ProducesResponseType(typeof(List<HitoProyectoDesarrollo>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<List<PlanificacionProyectoDesarrollo>>> GetAllByProyecto(int id)
        {
            try
            {
                var result = await _service.GetAllByIdProyecto(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Error retrieving data",
                    Detail = ex.Message
                });
            }
        }
    }
}
