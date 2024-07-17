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
    public class MarcaLicenciaConfiguration : IEntityTypeConfiguration<MarcaLicencia>
    {
        public void Configure(EntityTypeBuilder<MarcaLicencia> builder)
        {
            builder.ToTable("marca_licencia");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            //columnas
            builder.Property(e => e.Nombre)
                .HasColumnType("varchar(50)")
                .HasColumnName("nombre");

            builder.Property(e => e.Descripcion)
                .HasColumnType("varchar(15)")
                .HasColumnName("descripcion");


        }
    }
    
}
