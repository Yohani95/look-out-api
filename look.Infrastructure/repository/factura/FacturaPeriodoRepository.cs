using look.domain.entities.factura;
using look.domain.entities.soporte;
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
                .Include(p => p.Periodo).ThenInclude(p => p.Proyecto).ThenInclude(p => p.EmpresaPrestadora)
                .Include(p => p.Periodo).ThenInclude(p => p.Proyecto).ThenInclude(p => p.DiaPagos)
                .Include(p => p.HorasUtilizadas).ThenInclude(p => p.Soporte).ThenInclude(p => p.DiaPagos)
                .Include(p => p.Soporte).ThenInclude(p => p.DiaPagos)
                .Include(p => p.VentaLicencia).ThenInclude(p => p.DiaPagos)
                .Include(p => p.HitoProyectoDesarrollo).ThenInclude(h => h.ProyectoDesarrollo).ThenInclude(p => p.EmpresaPrestadora)
                .Include(p => p.Banco)
                .Where(p => p.IdEstado != EstadoFacturaPeriodo.ConstantesEstadoFactura.PENDIENTE)
                .Select(p => new FacturaPeriodo
                {
                    // Todos los campos de FacturaPeriodo se incluyen como están
                    Id = p.Id,
                    Rut = p.Rut,
                    RazonSocial = p.RazonSocial,
                    HesCodigo = p.HesCodigo,
                    OcCodigo = p.OcCodigo,
                    FechaHes = p.FechaHes,
                    FechaOc = p.FechaOc,
                    OrdenPeriodo = p.OrdenPeriodo,
                    Observaciones = p.Observaciones,
                    IdPeriodo = p.IdPeriodo,
                    Monto = p.Monto,
                    FechaFactura = p.FechaFactura,
                    FechaVencimiento = p.FechaVencimiento,
                    IdEstado = p.IdEstado,
                    IdHorasUtilizadas = p.IdHorasUtilizadas,
                    IdSoporteBolsa = p.IdSoporteBolsa,
                    idLicencia = p.idLicencia,
                    IdBanco = p.IdBanco,
                    IdHitoProyectoDesarrollo = p.IdHitoProyectoDesarrollo,
                    FechaPago = p.FechaPago,
                    Soporte = p.Soporte,
                    Periodo = p.Periodo,
                    HorasUtilizadas = p.HorasUtilizadas,
                    VentaLicencia = p.VentaLicencia,
                    Estado = p.Estado,
                    HitoProyectoDesarrollo = p.HitoProyectoDesarrollo,
                    Banco = p.Banco,
                    // Proyección específica para DocumentosFactura
                    DocumentosFactura = p.DocumentosFactura.Select(df => new DocumentosFactura
                    {
                        Id = df.Id,
                        idTipoDocumento = df.idTipoDocumento
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<FacturaPeriodo>> GetAllByIdPeriodo(int id)
        {
            return await _dbContext.FacturaPeriodo
                .Include(p => p.Periodo)
                 .Include(p => p.Estado)
                 .Include(fp => fp.DocumentosFactura)
                .Where(p => p.IdPeriodo == id)
                .ToListAsync();
        }

        public async Task<Boolean> ChangeEstado(int idPeriodo, int estado)
        {
            var facturas = await _dbContext.FacturaPeriodo
                   .Where(p => p.IdPeriodo == idPeriodo && p.IdEstado == EstadoFacturaPeriodo.ConstantesEstadoFactura.PENDIENTE)
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
        public async Task<Boolean> ChangeEstadoHoras(int idHoras, int estado)
        {
            var facturas = await _dbContext.FacturaPeriodo
                   .Where(p => p.IdHorasUtilizadas == idHoras && p.IdEstado == EstadoFacturaPeriodo.ConstantesEstadoFactura.PENDIENTE)
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
        public async Task<Boolean> ChangeEstadoSoporte(int idSoporte, int estado)
        {
            var facturas = await _dbContext.FacturaPeriodo
                   .Where(p => p.IdSoporteBolsa == idSoporte && p.IdEstado == EstadoFacturaPeriodo.ConstantesEstadoFactura.PENDIENTE)
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
        public async Task<List<FacturaPeriodo>> GetAllByIdHoras(int id)
        {
            return await _dbContext.FacturaPeriodo
                .Include(p => p.HorasUtilizadas)
                 .Include(p => p.Estado)
                 .Include(fp => fp.DocumentosFactura)
                .Where(p => p.IdHorasUtilizadas == id)
                .ToListAsync();
        }

        public async Task<List<FacturaPeriodo>> GetAllByIdSoporte(int id)
        {
            return await _dbContext.FacturaPeriodo
                .Include(p => p.Soporte)
                 .Include(p => p.Estado)
                 .Include(fp => fp.DocumentosFactura)
                .Where(p => p.IdSoporteBolsa == id)
                .ToListAsync();
        }

        public async Task<bool> ChangeEstadoByLicencia(int idlicencia, int estado)
        {
            var facturas = await _dbContext.FacturaPeriodo
                   .Where(p => p.idLicencia == idlicencia && p.IdEstado == EstadoFacturaPeriodo.ConstantesEstadoFactura.PENDIENTE)
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

        public async Task<List<FacturaPeriodo>> GetAllEntitiesByIdLicense(int id)
        {
            return await _dbContext.FacturaPeriodo
                .Include(p => p.Soporte)
                 .Include(p => p.Estado)
                 .Include(fp => fp.DocumentosFactura)
                .Where(p => p.idLicencia == id)
                .ToListAsync();
        }

        public async Task<List<FacturaPeriodo>> GetAllEntitiesByIdProyectoDesarrollo(int id)
        {
            return await _dbContext.FacturaPeriodo
            .Include(p => p.HitoProyectoDesarrollo)
             .Include(p => p.Estado)
             .Include(fp => fp.DocumentosFactura)
            .Where(p => p.IdHitoProyectoDesarrollo == id)
            .ToListAsync();
        }

        public async Task<bool> ChangeEstadoByProyectoDesarrollo(int idProyectoDesarrollo, int estado)
        {
            var facturas = await _dbContext.FacturaPeriodo
                  .Where(p => p.IdHitoProyectoDesarrollo == idProyectoDesarrollo && p.IdEstado == EstadoFacturaPeriodo.ConstantesEstadoFactura.PENDIENTE)
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

        public async Task<Dictionary<string, int>> GetFacturasResumenAsync()
        {
            var now = DateTime.Now;

            // Consulta para obtener los conteos agrupados
            var resumen = await _dbContext.FacturaPeriodo
                .GroupBy(fp => fp.IdEstado)
                .Select(g => new
                {
                    Estado = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // Conteos adicionales con condiciones específicas
            var enviadas = await _dbContext.FacturaPeriodo
                .Where(fp => fp.IdEstado == 5 && fp.FechaVencimiento < now)
                .CountAsync();

            var vencidas = await _dbContext.FacturaPeriodo
                .Where(fp => fp.IdEstado == 5 && fp.FechaVencimiento >= now)
                .CountAsync();

            // Mapeo a un diccionario para simplificar el acceso
            var resultado = new Dictionary<string, int>
    {
        { "Solicitadas", resumen.FirstOrDefault(r => r.Estado == 2)?.Count ?? 0 },
        { "Facturadas", resumen.FirstOrDefault(r => r.Estado == 3)?.Count ?? 0 },
        { "Enviadas", enviadas },
        { "Vencidas", vencidas },
        { "Pagadas", resumen.FirstOrDefault(r => r.Estado == 4)?.Count ?? 0 }
    };

            return resultado;
        }
    }
}
