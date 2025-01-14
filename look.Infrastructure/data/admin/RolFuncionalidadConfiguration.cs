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
    public class RolFuncionalidadConfiguration : IEntityTypeConfiguration<RolFuncionalidad>
    {
        public void Configure(EntityTypeBuilder<RolFuncionalidad> builder)
        {
            builder.ToTable("rol_funcionalidad");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            builder.Property(e => e.RolId)
                .HasColumnType("int")
                .HasColumnName("rol_id");

            builder.Property(e => e.FuncionalidadId)
                .HasColumnType("int")
                .HasColumnName("funcionalidad_id");

            builder.Property(e => e.TieneAcceso)
                .HasColumnType("boolean")
                .HasColumnName("tiene_acceso")
                .HasDefaultValue(false);
        }
    }
}
