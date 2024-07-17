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
    public class MarcaLicenciaContactoConfiguration : IEntityTypeConfiguration<MarcaLicenciaContacto>
    {
        public void Configure(EntityTypeBuilder<MarcaLicenciaContacto> builder)
        {
            builder.ToTable("marca_licencia_contacto");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            // Índices (hasIndex)
            builder.HasIndex(e => e.IdContacto, "FK_marca_licencia_contacto_persona");
            builder.HasIndex(e => e.IdMarca, "FK_marca_licencia_contacto_marca");

            //columnas
            builder.Property(e => e.IdMarca)
                .HasColumnType("int")
                .HasColumnName("idMarco");

            builder.Property(e => e.IdContacto)
                .HasColumnType("int")
                .HasColumnName("idContacto");
        }
    }
}
