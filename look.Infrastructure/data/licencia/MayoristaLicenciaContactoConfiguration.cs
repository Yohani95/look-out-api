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
    public class MayoristaLicenciaContactoConfiguration : IEntityTypeConfiguration<MayoristaLicenciaContacto>
    {
        public void Configure(EntityTypeBuilder<MayoristaLicenciaContacto> builder)
        {
            builder.ToTable("mayorista_licencia_contacto");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            // Índices (hasIndex)
            builder.HasIndex(e => e.IdMayorista, "FK_mayorista_licencia_contacto_mayorista");
            builder.HasIndex(e => e.IdContacto, "FK_mayorista_licencia_contacto_persona");

            //columnas
            builder.Property(e => e.IdContacto)
                .HasColumnType("int")
                .HasColumnName("id_contacto");

            builder.Property(e => e.IdMayorista)
                .HasColumnType("int")
                .HasColumnName("id_mayorista");

            //// Relaciones
        }
    }
}
