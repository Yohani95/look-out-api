using look.Application.interfaces;
using look.Application.interfaces.prospecto;
using look.domain.entities.prospecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.prospecto
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : BaseController<Empresa>
    {
        public EmpresaController(IEmpresaService service) : base(service)
        {
        }

        protected override int GetEntityId(Empresa entity)
        {
            return entity.Id;
        }
    }
}
