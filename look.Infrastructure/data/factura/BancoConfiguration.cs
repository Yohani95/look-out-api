using look.domain.entities.factura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.factura
{
    internal class BancoConfiguration : IEntityTypeConfiguration<Banco>
    {
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.ToTable("banco");
            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        }
    }
}
