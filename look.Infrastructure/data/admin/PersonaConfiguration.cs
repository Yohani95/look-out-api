using look.domain.entities.admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.admin
{
    internal class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Persona> entity)
        {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("persona");

                entity.HasIndex(e => e.TpeId, "FK_Persona_Tipo_Persona");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.PaiId)
                    .HasColumnType("int(11)")
                    .HasColumnName("pai_id");
                entity.Property(e => e.PerApellidoMaterno)
                    .HasMaxLength(50)
                    .HasColumnName("per_apellido_materno");
                entity.Property(e => e.PerApellidoPaterno)
                    .HasMaxLength(50)
                    .HasColumnName("per_apellido_paterno");
                entity.Property(e => e.PerFechaNacimiento).HasColumnName("per_fecha_nacimiento");
                entity.Property(e => e.PerIdNacional)
                    .HasMaxLength(50)
                    .HasColumnName("per_id_nacional");
                entity.Property(e => e.PerNombres)
                    .HasMaxLength(50)
                    .HasColumnName("per_nombres");
                entity.Property(e => e.TpeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tpe_id");
                entity.Property(e => e.Cargo)
                    .HasMaxLength(50)
                    .HasColumnName("cargo");
            entity.Property(e => e.PerfilId)
                    .HasColumnType("int(11)")
                    .HasColumnName("prf_id");

                entity.HasOne(d => d.TipoPersona).WithMany()
                    .HasForeignKey(d => d.TpeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Tipo_Persona");
        }
    }
}
