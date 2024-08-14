using look.Application.interfaces;
using look.Application.interfaces.prospecto;
using look.domain.entities.prospecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.prospecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustriaController : BaseController<Industria>
    {
        public IndustriaController(IIndustriaService service) : base(service)
        {
        }

        protected override int GetEntityId(Industria entity)
        {
            return entity.Id;
        }
    }
}
