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
    public class TipoLicenciaOportunidadConfiguration : IEntityTypeConfiguration<TipoLicenciaOportunidad>
    {
        public void Configure(EntityTypeBuilder<TipoLicenciaOportunidad> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.ToTable("tipo_licencia_oportunidad");
            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        }
    }
}
