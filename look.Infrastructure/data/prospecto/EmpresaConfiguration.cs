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
    internal class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            // Configuración de la clave primaria
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            // Configuración de las relaciones
            builder.HasOne(e => e.Industria)
                .WithMany(i => i.Empresas)
                .HasForeignKey(e => e.IdIndustria)
                .HasConstraintName("FK_empresa_industria");

            builder.HasMany(e => e.Prospectos)
                .WithOne(p => p.Empresa)
                .HasForeignKey(p => p.IdEmpresa)
                .HasConstraintName("FK_empresa_prospecto");
        }
    }
}
