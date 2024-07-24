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
            builder.HasIndex(e => e.IdFactura, "FK_factura_adaptacion");
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

            builder.Property(e => e.IdFactura)
                .HasColumnType("int")
                .HasColumnName("id_factura");

            builder.Property(e => e.Descripcion)
                .HasColumnType("varchar(255)")
                .HasColumnName("descripcion");

            builder.Property(e => e.MontoDiferencia)
                .HasColumnType("double")
                .HasColumnName("monto_diferencia");
            builder.HasOne(d => d.Factura)
                            .WithMany()
                            .HasForeignKey(d => d.IdFactura)
                            .HasConstraintName("FK_factura_adaptacion");
        }
    }
}
