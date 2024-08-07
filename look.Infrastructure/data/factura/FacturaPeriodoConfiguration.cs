using look.domain.entities.factura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.factura
{
    public class FacturaPeriodoConfiguration : IEntityTypeConfiguration<FacturaPeriodo>
    {
        public void Configure(EntityTypeBuilder<FacturaPeriodo> entity)
        {

            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("factura_periodo");
            entity.HasIndex(e => e.IdPeriodo, "FK_factura_periodo_periodo");
            entity.HasIndex(e => e.IdEstado, "FK_factura_periodo_estado");
            entity.HasIndex(e => e.IdHorasUtilizadas, "FK_factura_periodo_horas_utilizadas");
            entity.HasIndex(e => e.IdSoporteBolsa, "FK_factura_soporte");
            entity.HasIndex(e => e.idLicencia, "FK_factura_Licencia");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Rut)
               .HasColumnType("varchar(50)")
               .HasColumnName("rut");
            entity.Property(e => e.RazonSocial)
                .HasColumnType("varchar(120)")
                .HasColumnName("razon_social");
            entity.Property(e => e.HesCodigo)
                .HasColumnType("varchar(50)")
                .HasColumnName("hes_codigo");
            entity.Property(e => e.OcCodigo)
                .HasColumnType("varchar(50)")
                .HasColumnName("oc_codigo");
            entity.Property(e => e.FechaHes)
              .HasColumnType("datetime")
              .HasColumnName("fecha_hes");
            entity.Property(e => e.FechaOc)
                .HasColumnType("datetime")
                .HasColumnName("fecha_oc");
            entity.Property(e => e.OrdenPeriodo)
                .HasColumnType("int(11)")
                .HasColumnName("orden_periodo");
            entity.Property(e => e.Observaciones)
                .HasColumnType("varchar(200)")
                .HasColumnName("observaciones");
            entity.Property(e => e.IdPeriodo)
                .HasColumnType("int(11)")
                .HasColumnName("id_periodo");
            entity.Property(e => e.Monto)
                .HasColumnType("DOUBLE")
                .HasColumnName("monto");
            entity.Property(e => e.FechaFactura)
                .HasColumnType("datetime")
                .HasColumnName("fecha_factura");
            entity.Property(e => e.IdEstado)
                .HasColumnType("int(11)")
                .HasColumnName("id_estado");
            entity.Property(e => e.IdHorasUtilizadas)
                .HasColumnType("int(11)")
                .HasColumnName("id_horas_utilizadas");
            entity.Property(e => e.IdSoporteBolsa)
                .HasColumnType("int(11)")
                .HasColumnName("id_soporte_bolsa");
            entity.Property(e => e.FechaVencimiento)
             .HasColumnType("datetime")
             .HasColumnName("fecha_vencimiento");

            entity.Property(e => e.idLicencia)
           .HasColumnType("int")
           .HasColumnName("id_licencia");

            //realaciones
            entity.HasOne(d => d.Periodo).WithMany()
                .HasForeignKey(d => d.IdPeriodo)
                .HasConstraintName("FK_factura_periodo_periodos");
            entity.HasOne(d => d.HorasUtilizadas).WithMany()
                .HasForeignKey(d => d.IdHorasUtilizadas)
                .HasConstraintName("FK_factura_periodo_horas_utilizadas");
            entity.HasOne(d => d.Soporte).WithMany()
            .HasForeignKey(d => d.IdSoporteBolsa)
            .HasConstraintName("FK_factura_soporte");
            entity.HasOne(d => d.Estado).WithMany()
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_factura_periodo_estado");
            entity.HasMany(d => d.DocumentosFactura)
                .WithOne(d => d.FacturaPeriodo)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_documentos_factura_factura_periodo");
            entity.HasOne(d => d.VentaLicencia).WithMany()
                .HasForeignKey(d => d.idLicencia)
                .HasConstraintName("FK_factura_Licencia");



        }
    }
}
