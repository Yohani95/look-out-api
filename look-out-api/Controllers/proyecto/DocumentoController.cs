using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            // Implementa la lógica para obtener el ID de la entidad Documento
            return entity.DocId;
        }
        
        [HttpPost("SendDocuments/{idProyecto}/{idCliente}")]
        public async Task<IActionResult> SendDocuments([FromForm] List<IFormFile> files,int idProyecto,int idCliente)
        {
            try
            {
                var result = await _documentoService.SendDocuments(files,idProyecto,idCliente);
                switch (result.MessageCode)
                {
                    case ServiceResultMessage.Success:
                        return Ok(result);
                    case ServiceResultMessage.InvalidInput:
                        return BadRequest(result);
                    case ServiceResultMessage.NotFound:
                        return NotFound(result);
                    default:
                        return StatusCode(500, result);
                }
            }
            catch(Exception ex)
            {
                Log.Error("ERROR creando servicio"+ ex.Message);
                return StatusCode(500);
            }
        }
    }
    
    
}
