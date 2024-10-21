
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
            builder.HasIndex(e => e.IdLicencia, "FK_licencia_tarifario");
            builder.HasIndex(e => e.IdMoneda, "FK_licencia_tarifario_moneda");
            //columnas
            builder.Property(e => e.FechaTermino)
                .HasColumnType("datetime")
                .HasColumnName("fecha_termino");

            builder.Property(e => e.FechaVigencia)
                .HasColumnType("datetime")
                .HasColumnName("fecha_vigencia");

            builder.Property(e => e.Valor)
                .HasColumnType("double")
                .HasColumnName("valor");

            builder.Property(e => e.IdLicencia)
                .HasColumnType("int")
                .HasColumnName("id_licencia");

            builder.Property(e => e.IdMarcaLicencia)
                .HasColumnType("int")
                .HasColumnName("id_marca_licencia");

            builder.Property(e => e.IdMayoristaLicencia)
                .HasColumnType("int")
                .HasColumnName("id_mayorista_licencia");

            builder.Property(e => e.IdVentaLicencia)
               .HasColumnType("int")
               .HasColumnName("id_venta_licencia");
            builder.Property(e => e.IdMoneda)
                .HasColumnType("int")
                .HasColumnName("id_moneda");
            builder.Property(e => e.Cantidad)
                .HasColumnType("int")
                .HasColumnName("cantidad");
            //// Relaciones
            builder.HasOne(d => d.MarcaLicencia).WithMany()
                .HasForeignKey(d => d.IdMarcaLicencia)
                .HasConstraintName("FK_marca_licencia");

            builder.HasOne(d => d.MayoristaLicencia).WithMany()
                .HasForeignKey(d => d.IdMayoristaLicencia)
                .HasConstraintName("FK_mayorista_licencia");

            builder.HasOne(d => d.TipoLicencia).WithMany()
                .HasForeignKey(d => d.IdLicencia)
                .HasConstraintName("FK_licencia_tarifario");

            builder.HasOne(d => d.Moneda).WithMany()
                .HasForeignKey(d => d.IdMoneda)
                .HasConstraintName("FK_licencia_tarifario_moneda");

        }
    }
}
