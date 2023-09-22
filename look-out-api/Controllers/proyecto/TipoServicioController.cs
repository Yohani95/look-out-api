using look.Application.interfaces;
using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicioController : BaseController<TipoServicio>
    {
        public TipoServicioController(ITipoServicioService service) : base(service)
        {
        }

        protected override int GetEntityId(TipoServicio entity)
        {
            throw new NotImplementedException();
        }
    }
}
