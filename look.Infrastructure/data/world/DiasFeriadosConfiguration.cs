using look.domain.entities.world;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.world
{
    public class DiasFeriadosConfiguration : IEntityTypeConfiguration<DiasFeriados>
    {
        public void Configure(EntityTypeBuilder<DiasFeriados> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.ToTable("dias_feriados");
            builder.HasIndex(e => e.IdPais, "FK_dias_pais");
            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Fecha)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("fecha");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("nombre");

            builder.Property(e => e.Pais)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("pais");

            builder.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");

            builder.Property(e => e.IdPais)
                .HasColumnType("int")
                .HasColumnName("id_pais")
                .ValueGeneratedOnAdd();
        }
    }
}
