using look.domain.entities.prospecto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.prospecto
{
    internal class ProspectoConfiguration : IEntityTypeConfiguration<Prospecto>
    {
        public void Configure(EntityTypeBuilder<Prospecto> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.ToTable("prospecto");

            // Configuración de las propiedades
            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_creacion");

            builder.Property(p => p.FechaActividad)
           .HasColumnType("timestamp")
           .HasColumnName("fecha_actividad")
           .HasDefaultValueSql("CURRENT_TIMESTAMP")
           .ValueGeneratedOnAddOrUpdate();

            // Configuración de relaciones
            builder.HasOne(p => p.Kam)
                .WithMany()
                .HasForeignKey(p => p.IdKam)
                .HasConstraintName("FK_prospecto_kam");

            builder.HasOne(p => p.Empresa)
                .WithMany(e => e.Prospectos)
                .HasForeignKey(p => p.IdEmpresa)
                .HasConstraintName("FK_prospecto_empresa");

            builder.HasOne(p => p.ContactoProspecto)
                .WithMany(cp => cp.Prospectos)
                .HasForeignKey(p => p.IdContactoProspecto)
                .HasConstraintName("FK_prospecto_contacto_prospecto");

            builder.HasOne(p => p.EstadoProspecto)
                .WithMany(cp => cp.Prospectos)
                .HasForeignKey(p => p.IdEstadoProspecto)
                .HasConstraintName("FK_prospecto_estado_prospecto");
        }
    }
}
