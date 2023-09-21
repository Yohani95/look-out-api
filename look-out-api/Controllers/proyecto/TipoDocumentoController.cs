using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : BaseController<TipoDocumento>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly ITipoDocumentoService _tipoDocumentoService;
        public TipoDocumentoController(ITipoDocumentoService tipoDocumentoService) : base(tipoDocumentoService)
        {
            _tipoDocumentoService = tipoDocumentoService;
        }

        protected override int GetEntityId(TipoDocumento entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Moneda
            return entity.TdoId;
        }
    }
}
