using look.domain.entities.licencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.licencia
{
    public class EstadoVentaLicenciaConfiguration : IEntityTypeConfiguration<EstadoVentaLicencia>
    {
        public void Configure(EntityTypeBuilder<EstadoVentaLicencia> builder)
        {
            builder.ToTable("estado_venta_licencia");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            builder.Property(e => e.Nombre)
                .HasColumnType("varchar(100)")
                .HasColumnName("nombre");

            builder.Property(e => e.Descripcion)
                .HasColumnType("varchar(255)")
                .HasColumnName("descripcion");
        }
    }
}
