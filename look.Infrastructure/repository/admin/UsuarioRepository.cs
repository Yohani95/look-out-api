using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.admin
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Usuario>> GetComplete()
        {
            return await _dbContext.Usuario.Include(u=>u.Perfil).ToListAsync();
        }

        public async Task<Usuario> Login(Usuario user)
        {
            return await _dbContext.Usuario
                .Include(u => u.Persona)
                .Include(u=>u.Perfil)
                .Include(u=>u.Rol)
                .FirstOrDefaultAsync(u => u.UsuNombre==user.UsuNombre  && u.UsuContraseña == user.UsuContraseña);
        }
    }
}
