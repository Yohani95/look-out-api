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
    public class EstadoReunionProspectoConfiguration : IEntityTypeConfiguration<EstadoReunionProspecto>
    {
        public void Configure(EntityTypeBuilder<EstadoReunionProspecto> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.ToTable("estado_reunion_prospecto");
            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        }
    }
}
