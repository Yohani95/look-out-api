using look.domain.entities.licencia;
using look.domain.entities.oportunidad;
using look.domain.interfaces.licencia;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.licencia
{
    public class VentaLicenciaRepository : Repository<VentaLicencia>, IVentaLicenciaRepository
    {
        public VentaLicenciaRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<IEnumerable<VentaLicencia>> GetAllAsync()
        {
            // Calcular los montos por VentaLicencia
            var montos = await _dbContext.TarifarioVentaLicencias
                .GroupBy(t => t.IdVentaLicencia)
                .Select(g => new
                {
                    IdVentaLicencia = g.Key,
                    TotalMonto = g.Sum(t => (t.Cantidad ?? 1) * t.Valor) // Usa 1 si Cantidad es null
                })
                .ToListAsync();

            // Recuperar las VentaLicencia
            var ventas = await _dbContext.VentaLicencias
                .Include(o => o.Cliente)
                .Include(o => o.EstadoVentaLicencia)
                .Include(o => o.EmpresaPrestadora)
                .Include(o => o.Moneda)
                .Include(o => o.Pais)
                .Include(o => o.Kam)
                .ToListAsync(); // Ejecutamos la consulta aquí para evitar la traducción de LINQ compleja

            // Asignar los montos en memoria
            foreach (var venta in ventas)
            {
                var monto = montos.FirstOrDefault(m => m.IdVentaLicencia == venta.Id)?.TotalMonto ?? 0;
                venta.Monto = monto; // Asigna el monto calculado
            }

            return ventas;
        }
        public new async Task<VentaLicencia> GetByIdAsync(int id)
        {
            return await _dbContext.VentaLicencias
                .Include(o => o.Cliente)
                .Include(o => o.EstadoVentaLicencia)
                .Include(o => o.EmpresaPrestadora)
                .Include(o => o.Moneda)
                .Include(o => o.Pais)
                .Include(o => o.Kam)
                .FirstAsync(v => v.Id == id);
        }
    }
}
