using look.Application.interfaces;
using look.Application.interfaces.cuentas;
using look.domain.entities.cuentas;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.cuentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : BaseController<Cliente>
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService service) : base(service)
        {
            _clienteService = service;
        }
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllWithEntities()
        {
            var persona = await _clienteService.GetAllWithEntities();

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        protected override int GetEntityId(Cliente entity)
        {
            return entity.CliId;
        }
    }
}
