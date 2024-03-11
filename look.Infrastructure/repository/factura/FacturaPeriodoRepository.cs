using look.domain.entities.factura;
using look.domain.interfaces.factura;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.factura
{
    public class FacturaPeriodoRepository : Repository<FacturaPeriodo>, IFacturaPeriodoRepository
    {
        public FacturaPeriodoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<FacturaPeriodo>> GetAllByPreSolicitada()
        {
            return await _dbContext.FacturaPeriodo
                .Include(p => p.Estado)
                .Include(p=>p.Periodo).ThenInclude(p=>p.Proyecto).ThenInclude(p=>p.EmpresaPrestadora)
                .Include(p => p.Periodo).ThenInclude(p => p.Proyecto).ThenInclude(p => p.DiaPagos)
                .Include(fp => fp.DocumentosFactura)
                .Where(p => p.IdEstado != EstadoFacturaPeriodo.ConstantesEstadoFactura.PENDIENTE)
                .ToListAsync();
        }

        public async Task<List<FacturaPeriodo>> GetAllByIdPeriodo(int id)
        {
            return await _dbContext.FacturaPeriodo
                .Include(p=>p.Periodo)
                 .Include(p => p.Estado)
                 .Include(fp => fp.DocumentosFactura)
                .Where(p => p.IdPeriodo == id)
                .ToListAsync();
        }

        public async Task<Boolean> ChangeEstado(int idPeriodo, int estado)
        {
            var facturas = await _dbContext.FacturaPeriodo
                   .Where(p => p.IdPeriodo == idPeriodo)
                   .ToListAsync();

            if (facturas != null && facturas.Any())
            {
                foreach (var factura in facturas)
                {
                    factura.IdEstado = estado;
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
