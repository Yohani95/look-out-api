using look.Application.interfaces.admin;
using look.Application.services.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.ListComplete();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(Usuario usuario)
        {
            await _usuarioService.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.UsuId }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuId)
            {
                return BadRequest();
            }
            await _usuarioService.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            await _usuarioService.DeleteAsync(usuario);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login usuario)
        {
            Usuario user = new Usuario();
            user.UsuNombre= usuario.UsuNombre;
            user.UsuContraseña = usuario.UsuContraseña;
            var loggedUser = await _usuarioService.Login(user);
            if (loggedUser == null)
            {
                return Unauthorized("Credenciales invalidas");
            }
            return Ok(loggedUser);
        }
    }
}
