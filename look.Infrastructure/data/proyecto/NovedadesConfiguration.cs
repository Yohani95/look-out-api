using look.domain.entities.proyecto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.proyecto
{
    public class NovedadesConfiguration : IEntityTypeConfiguration<Novedades>
    {
        public void Configure(EntityTypeBuilder<Novedades> entity)
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
            entity.ToTable("novedades");
            entity.HasIndex(e => e.idProyecto, "FK_proyecto_proyecto");
            entity.HasIndex(e => e.idPersona, "FK_proyecto_persona");
            entity.HasIndex(e => e.IdPerfil, "FK_proyecto_perfil");
            entity.HasIndex(e => e.IdTipoNovedad, "FK_proyecto_tipo_novedad");
            entity.HasIndex(e => e.idProfesionalProyecto, "FK_novedad_participante");

            entity.Property(e => e.id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.idPersona)
                .HasColumnType("int(11)")
                .HasColumnName("idPersona");
            entity.Property(e => e.idProyecto)
                .HasColumnType("int(11)")
                .HasColumnName("idProyecto");
            entity.Property(e => e.fechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.fechaHasta)
                .HasColumnType("datetime")
                .HasColumnName("fechaHasta");
            entity.Property(e => e.observaciones)
                .HasColumnType("text")
                .HasColumnName("observaciones");
            entity.Property(e => e.IdPerfil)
                .HasColumnType("int(11)")
                .HasColumnName("idperfil");
            entity.Property(e => e.IdTipoNovedad)
                .HasColumnType("int(11)")
                .HasColumnName("idTipoNovedad");
            entity.Property(e => e.idProfesionalProyecto)
                   .HasColumnType("int(11)")
                   .HasColumnName("idProfesionalProyecto");
            entity.HasOne(d => d.Persona).WithMany()
                    .HasForeignKey(d => d.idPersona)
                    .HasConstraintName("FK_proyecto_persona");

            entity.HasOne(d => d.Perfil).WithMany()
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("FK_proyecto_perfil");

            entity.HasOne(d => d.Proyecto).WithMany()
                .HasForeignKey(d => d.idProyecto)
                .HasConstraintName("FK_proyecto_proyecto");

            entity.HasOne(d => d.TipoNovedades).WithMany()
                .HasForeignKey(d => d.IdTipoNovedad)
                .HasConstraintName("FK_proyecto_tipo_novedad");
        }
    }
}
