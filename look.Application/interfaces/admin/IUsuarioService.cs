using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.admin
{
    public interface IUsuarioService:IService<Usuario>
    {
        Task<Usuario> Login(Usuario usuario);
        
        Task encriptarPassword(Usuario usuario);
        
        Task ActualizaUsuario(Usuario usuario);
        Task<List<Usuario>> ListComplete();
    }
}
