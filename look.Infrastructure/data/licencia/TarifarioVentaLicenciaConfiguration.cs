using look.domain.entities.licencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.licencia
{
    public class TarifarioVentaLicenciaConfiguration : IEntityTypeConfiguration<TarifarioVentaLicencia>
    {
        public void Configure(EntityTypeBuilder<TarifarioVentaLicencia> builder)
        {
            builder.ToTable("tarifario_venta_licencia");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            // Índices (hasIndex)
            builder.HasIndex(e => e.IdMarcaLicencia, "FK_marca_licencia");
            builder.HasIndex(e => e.IdMayoristaLicencia, "FK_mayorista_licencia");

            //columnas
            builder.Property(e => e.FechaTermino)
                .HasColumnType("datetime")
                .HasColumnName("fecha_termino");

            builder.Property(e => e.FechaVigencia)
                .HasColumnType("datetime")
                .HasColumnName("fecha_vigencia");
            
            builder.Property(e => e.Valor)
                .HasColumnType("double")
                .HasColumnName("double");

            //// Relaciones
            builder.HasOne(d => d.MarcaLicencia).WithMany()
                .HasForeignKey(d => d.IdMarcaLicencia)
                .HasConstraintName("FK_marca_licencia");

            builder.HasOne(d => d.MayoristaLicencia).WithMany()
                .HasForeignKey(d => d.IdMayoristaLicencia)
                .HasConstraintName("FK_mayorista_licencia");

        }
    }
}
