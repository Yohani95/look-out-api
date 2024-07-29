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
    public class FacturaAdaptacionConfiguration : IEntityTypeConfiguration<FacturaAdaptacion>
    {
        public void Configure(EntityTypeBuilder<FacturaAdaptacion> builder)
        {
            builder.ToTable("factura_adaptacion");

            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.HasIndex(e => e.IdCliente, "FK_factura_adaptacion_cliente");
            builder.HasIndex(e => e.IdHorasUtilizadas, "FK_factura_adaptacion_horas_utilizadas");
            builder.HasIndex(e => e.IdLicencia, "FK_factura_adaptacion_licencia");
            builder.HasIndex(e => e.IdPeriodoProyecto, "FK_factura_adaptacion_periodo_proyecto");
            builder.HasIndex(e => e.IdSoporte, "FK_factura_adaptacion_soporte");
            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            builder.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp")
                .HasColumnName("fecha_creacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Monto)
                .HasColumnType("double")
                .HasColumnName("monto");

            builder.Property(e => e.IdCliente)
                .HasColumnType("int")
                .HasColumnName("id_cliente");

            builder.Property(e => e.Descripcion)
                .HasColumnType("varchar(255)")
                .HasColumnName("descripcion");

            builder.Property(e => e.IdSoporte)
                 .HasColumnType("int")
                 .HasColumnName("id_soporte");

            builder.Property(e => e.IdHorasUtilizadas)
                .HasColumnType("int")
                .HasColumnName("id_horas_utilizadas");

            builder.Property(e => e.IdLicencia)
                .HasColumnType("int")
                .HasColumnName("id_licencia");

            builder.Property(e => e.IdPeriodoProyecto)
                .HasColumnType("int")
                .HasColumnName("id_periodo_proyecto");


            builder.Property(e => e.MontoDiferencia)
                .HasColumnType("double")
                .HasColumnName("monto_diferencia");
            builder.HasOne(d => d.Cliente)
                            .WithMany()
                            .HasForeignKey(d => d.IdCliente)
                            .HasConstraintName("FK_factura_adaptacion_cliente");
        }
    }
}
