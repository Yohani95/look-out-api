using look.Application.interfaces;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class OportunidadController : BaseController<Oportunidad>
    {
        public OportunidadController(IService<Oportunidad> service) : base(service)
        {
        }

        protected override int GetEntityId(Oportunidad entity)
        {
            return entity.Id;
        }
    }
}
