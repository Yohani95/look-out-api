using look.Application.interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        private readonly IService<T> _service;

        protected BaseController(IService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(T entity)
        {
            try
            {
                Log.Information("[Create] Solicitud creacion de entidad: " + entity.ToString());
                var result = await _service.AddAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = GetEntityId(entity) }, entity);
            }catch(Exception e)
            {
                Log.Error("[Create] Error al crear entidad: " + entity.ToString() + " Error: " + e.Message);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, T entity)
        {
            try
            {
                Log.Information("[Update] Solicitud actualizacion de entidad: " + entity.ToString());
                if (id != GetEntityId(entity))
                {
                    return BadRequest();
                }
                await _service.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                Log.Error("[Update] Error al actualizar entidad: " + entity.ToString() + " Error: " + e.Message);
                
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(entity);
            return NoContent();
        }

        protected abstract int GetEntityId(T entity);
    }
}
