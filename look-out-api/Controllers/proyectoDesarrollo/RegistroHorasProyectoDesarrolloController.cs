using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroHorasProyectoDesarrolloController : BaseController<RegistroHorasProyectoDesarrollo>
    {
        private readonly IRegistroHorasProyectoDesarrolloService _service;
        public RegistroHorasProyectoDesarrolloController(IRegistroHorasProyectoDesarrolloService service) : base(service)
        {
            _service = service;
        }

        protected override int GetEntityId(RegistroHorasProyectoDesarrollo entity)
        {
            return entity.Id;
        }
        [HttpGet("GetByIdProfesional/{id}")]
        [ProducesResponseType(typeof(List<HitoProyectoDesarrollo>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<List<RegistroHorasProyectoDesarrollo>>> GetByIdProfesional(int id)
        {
            try
            {
                var result = await _service.GetByIdProfesional(id);
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
