using look.domain.dto.cuentas;
using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.interfaces.cuentas;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.Cuentas
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> CreateWithEntities(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cliente>> GetAllWithEntities()
        {
            // Obtiene todos los clientes con las relaciones definidas
            var clientes = await _dbContext.Cliente
                .Include(c => c.EstadoCliente)
                .Include(c => c.Giro)
                .Include(c => c.Pais)
                    .ThenInclude(p => p.Lenguaje)
                .Include(c => c.SectorComercial)
                .OrderBy(c => c.CliId)
                .ToListAsync();

            // Carga la relación Kam desde la tabla intermedia
            var clienteIds = clientes.Select(c => c.CliId).ToList();

            var kams = await (from cp in _dbContext.ClientePersona
                              join p in _dbContext.Persona on cp.PerId equals p.Id
                              where clienteIds.Contains((int)cp.CliId) && p.TpeId == 2 // Solo personas tipo Kam
                              select new { cp.CliId, Kam = p })
                             .ToListAsync();

            // Asigna el Kam a los clientes correspondientes
            foreach (var cliente in clientes)
            {
                var kam = kams.FirstOrDefault(k => k.CliId == cliente.CliId)?.Kam;
                cliente.Kam = kam;
            }

            return clientes;
        }
        public async Task<List<CuentaDTO>> GetAllClientDTO()
        {
            var query = from c in _dbContext.Cliente
                              .Include(c => c.SectorComercial)
                              .Include(c => c.Pais)
                        join e in _dbContext.Email.Where(email => email.EmaPrincipal == 1)
                            on c.CliId equals e.CliId into emailGroup
                        from email in emailGroup.DefaultIfEmpty()
                        join cp in _dbContext.ClientePersona
                            on c.CliId equals cp.CliId into clientePersonaGroup
                        from clientePersona in clientePersonaGroup.DefaultIfEmpty()
                        join p in _dbContext.Persona
                            on clientePersona.PerId equals p.Id into personaGroup
                        from persona in personaGroup.DefaultIfEmpty()
                        where persona == null || persona.TpeId == 2 // Solo personas tipo Kam
                        select new
                        {
                            Cliente = c,
                            Email = email.EmaEmail,
                            Kam = persona
                        };

            var result = await query.OrderBy(dto => dto.Cliente.CliId).ToListAsync();

            // Asignar el Kam a la propiedad en Cliente
            foreach (var item in result)
            {
                item.Cliente.Kam = item.Kam; // Asignar el Kam dentro del cliente
            }

            // Mapear los resultados a CuentaDTO
            var cuentaDTOs = result.Select(r => new CuentaDTO
            {
                Cliente = r.Cliente,
                email = r.Email
            }).ToList();

            return cuentaDTOs;
        }
    }
}
