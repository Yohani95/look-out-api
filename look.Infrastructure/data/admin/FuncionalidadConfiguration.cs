using look.domain.entities.admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.admin
{
    internal class FuncionalidadConfiguration : IEntityTypeConfiguration<Funcionalidad>
    {
        public void Configure(EntityTypeBuilder<Funcionalidad> builder)
        {
            builder.ToTable("funcionalidad");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            builder.Property(e => e.Nombre)
                .HasColumnType("varchar(50)")
                .HasColumnName("nombre");

            builder.Property(e => e.Descripcion)
                .HasColumnType("varchar(100)")
                .HasColumnName("descripcion");
        }
    }
}
