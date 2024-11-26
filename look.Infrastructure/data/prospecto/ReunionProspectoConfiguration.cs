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
    public class ReunionProspectoConfiguration : IEntityTypeConfiguration<ReunionProspecto>
    {
        public void Configure(EntityTypeBuilder<ReunionProspecto> builder)
        {
            builder.Property(e => e.Id)
              .HasColumnType("int(11)")
              .HasColumnName("id");

            builder.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_creacion");

            builder.Property(e => e.IdEstadoReunionProspecto)
               .HasColumnType("int)")
               .HasColumnName("IdEstadoReunionProspecto");
        }
    }
}
