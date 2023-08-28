using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;


namespace look.Application.services.admin
{
    public class UsuarioService : Service<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository repository) : base(repository)
        {
            _usuarioRepository = repository;
        }

        public async Task<List<Usuario>> ListComplete()
        {
            return await _usuarioRepository.GetComplete();
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            return await _usuarioRepository.Login(usuario);
        }
    }
}
