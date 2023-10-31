using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class TelefonoRepository: Repository<Telefono>, ITelefonoRepository
    {
        public TelefonoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Telefono>> GetComplete()
        {
            return await _dbContext.Telefono
                .Include(u=>u.tipoTelefono)
                .Include(u=>u.cliente)
                .Include(u=>u.persona).ToListAsync();
        }
        
        public async Task<List<Telefono>> ListCompleteByIdPersona(int id)
        {
            return await _dbContext.Telefono.Include(u=>u.tipoTelefono).Include(u=>u.cliente).Include(u=>u.persona).Where(p => p.perId == id).ToListAsync();
        }
    }
}

