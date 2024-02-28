using look.Application.interfaces;
using look.Application.interfaces.proyecto;
using look.Application.services;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaPrestadoraController : BaseController<EmpresaPrestadora>
    {
        public EmpresaPrestadoraController(IEmpresaPrestadoraService service) : base(service)
        {
        }

        protected override int GetEntityId(EmpresaPrestadora entity)
        {
            return entity.Id;
        }
    }
}
