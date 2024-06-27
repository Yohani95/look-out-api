using look.domain.entities.oportunidad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.oportunidad
{
    public class NovedadOportunidadConfiguration : IEntityTypeConfiguration<NovedadOportunidad>
    {
        public void Configure(EntityTypeBuilder<NovedadOportunidad> builder)
        {
            builder.ToTable("novedad_oportunidad");
            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Fecha).HasColumnName("fecha");
            builder.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(255);
            builder.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(255);
            builder.Property(e => e.IdOportunidad).HasColumnName("id_oportunidad");
        }
    }
}
