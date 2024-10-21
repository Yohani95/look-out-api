using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaPeriodoController : BaseController<FacturaPeriodo>
    {
        private readonly IFacturaPeriodoService _service;
        public FacturaPeriodoController(IFacturaPeriodoService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("GetAllEntitiesByIdPeriod/{id}")]
        public async Task<ActionResult<FacturaPeriodo>> GetAllEntitiesByIdPeriod(int id)
        {
            Log.Information("[GetAllEntitiesByIdPeriod] Solicitud getall facturas periodos IDPERIODO : " + id);
            var data = await _service.GetAllEntitiesByIdPeriod(id);
            return Ok(data);
        }
        [HttpGet("GetAllEntitiesByIdHoras/{id}")]
        public async Task<ActionResult<FacturaPeriodo>> GetAllEntitiesByIdHoras(int id)
        {
            Log.Information("[GetAllEntitiesByIdHoras] Solicitud getall dfacturas periodos IDHORAS : " + id);
            var data = await _service.GetAllByIdHoras(id);
            return Ok(data);
        }

        [HttpGet("GetAllEntitiesByIdSoporte/{id}")]
        public async Task<ActionResult<FacturaPeriodo>> GetAllEntitiesByIdSoporte(int id)
        {
            Log.Information("[GetAllEntitiesByIdSoporte] Solicitud getall dfacturas periodos IDHORAS : " + id);
            var data = await _service.GetAllByIdSoporte(id);
            return Ok(data);
        }

        [HttpGet("GetAllByPreSolicitada")]
        public async Task<ActionResult<FacturaPeriodo>> GetAllByPreSolicitada()
        {
            Log.Information("[GetAllByPreSolicitada] Solicitud getall facturas diferente a pendiente ");
            var data = await _service.GetAllByPreSolicitada();
            return Ok(data);
        }
        [HttpGet("ChangeEstado/{idPeriodo}/{estado}")]
        public async Task<IActionResult> ChangeEstado(int idPeriodo, int estado)
        {
            Log.Information("[ChangeEstado] Cambiar estado idPeriodo: " + idPeriodo + ", Estado ID:" + estado);
            var data = await _service.ChangeEstado(idPeriodo, estado);
            return Ok(data);
        }
        [HttpGet("ChangeEstadoHoras/{idHoras}/{estado}")]
        public async Task<IActionResult> ChangeEstadoHoras(int idHoras, int estado)
        {
            Log.Information("[ChangeEstadoHoras] Cambiar estado idHoras: " + idHoras + ", Estado ID:" + estado);
            var data = await _service.ChangeEstadoHoras(idHoras, estado);
            return Ok(data);
        }
        [HttpGet("ChangeEstadoSoporte/{idSoporte}/{estado}")]
        public async Task<IActionResult> ChangeEstadoSoporte(int idSoporte, int estado)
        {
            Log.Information("[ChangeEstadoSoporte] Cambiar estado idSoporte: " + idSoporte + ", Estado ID:" + estado);
            var data = await _service.ChangeEstadoSoporte(idSoporte, estado);
            return Ok(data);
        }

        [HttpGet("ChangeEstadoLicencia/{idLicencia}/{estado}")]
        public async Task<IActionResult> ChangeEstadoLicencia(int idLicencia, int estado)
        {
            Log.Information("[ChangeEstadoLicencia] Cambiar estado idLicencia: " + idLicencia + ", Estado ID:" + estado);
            var data = await _service.ChangeEstadoByLicencia(idLicencia, estado);
            return Ok(data);
        }
        [HttpGet("GetAllEntitiesByIdLicense/{id}")]
        public async Task<ActionResult<FacturaPeriodo>> GetAllEntitiesByIdLicense(int id)
        {
            Log.Information("[GetAllEntitiesByIdLicense] Solicitud getall facturas licencia idLicencia: " + id);
            var data = await _service.GetAllEntitiesByIdLicense(id);
            return Ok(data);
        }

        [HttpGet("ChangeEstadoProyectoDesarrollo/{idProyectoDesarrollo}/{estado}")]
        public async Task<IActionResult> ChangeEstadoProyectoDesarrollo(int idProyectoDesarrollo, int estado)
        {
            Log.Information("[ChangeEstadoProyectoDesarrollo] Cambiar estado idProyectoDesarrollo: " + idProyectoDesarrollo + ", Estado ID:" + estado);
            var data = await _service.ChangeEstadoByProyectoDesarrollo(idProyectoDesarrollo, estado);
            return Ok(data);
        }
        [HttpGet("GetAllEntitiesByIdHitoProyectoDesarrollo/{id}")]
        public async Task<ActionResult<List<FacturaPeriodo>>> GetAllEntitiesByIdHitoProyectoDesarrollo(int id)
        {
            Log.Information("[GetAllEntitiesByIdProyectoDesarrollo] Solicitud getall facturas proyectoDesarrollo idProyectoDesarrollo: " + id);
            var data = await _service.GetAllEntitiesByIdProyectoDesarrollo(id);
            return Ok(data);
        }
        protected override int GetEntityId(FacturaPeriodo entity)
        {
            return entity.Id;
        }
    }
}
