using look.Application.interfaces;
using look.Application.interfaces.licencia;
using look.domain.entities.licencia;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.licencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaLicenciaContactoController : BaseController<MarcaLicenciaContacto>
    {
        public MarcaLicenciaContactoController(IMarcaLicenciaContactoService service) : base(service)
        {
        }

        protected override int GetEntityId(MarcaLicenciaContacto entity)
        {
            return entity.Id;
        }
    }
}
