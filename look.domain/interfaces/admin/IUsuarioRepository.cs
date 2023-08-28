using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.admin
{
    public interface IUsuarioRepository:IRepository<Usuario>
    {
        Task<Usuario> Login(Usuario usuario);
        Task<List<Usuario>> GetComplete();
    }
}
