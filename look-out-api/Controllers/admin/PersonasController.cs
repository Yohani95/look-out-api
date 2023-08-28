using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : BaseController<Persona>
    {
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService service) : base(service)
        {
            _personaService = service;
        }
        [HttpGet("kam/{id}")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonaByKam(int id)
        {
            var persona = await _personaService.GetAllByType(2);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        protected override int GetEntityId(Persona entity)
        {
           return entity.Id;
        }
    }
}
