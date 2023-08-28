using look.Application.interfaces;
using look.Application.interfaces.cuentas;
using look.domain.entities.cuentas;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.cuentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoClientesController : BaseController<EstadoCliente>
    {

        public EstadoClientesController(IEstadoClienteService service) : base(service)
        {
        }


        protected override int GetEntityId(EstadoCliente entity)
        {
            return entity.EclId;
        }
    }
}
