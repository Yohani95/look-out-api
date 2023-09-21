using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : BaseController<Documento>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IDocumentoService _documentoService;
        public DocumentoController(IDocumentoService documentoService) : base(documentoService)
        {
            _documentoService = documentoService;
        }

        protected override int GetEntityId(Documento entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Moneda
            return entity.DocId;
        }
    }
}
