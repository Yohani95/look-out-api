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
    internal class ContactoProspectoConfiguration : IEntityTypeConfiguration<ContactoProspecto>
    {
        public void Configure(EntityTypeBuilder<ContactoProspecto> builder)
        {
            // Configuración de la tabla
            builder.ToTable("contactoprospectos");

            // Configuración de la clave primaria
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            // Configuración de propiedades
            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.NombreCompleto)
                .HasMaxLength(150)
                .HasColumnName("NombreCompleto");

            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("Email");

            builder.Property(e => e.Numero)
                .HasMaxLength(20)
                .HasColumnName("Numero");

            builder.Property(e => e.PerfilLinkedin)
                .HasMaxLength(200)
                .HasColumnName("PerfilLinkedin");

            builder.Property(e => e.IdTipo)
                .HasColumnType("int(11)")
                .HasColumnName("IdTipo");

            builder.Property(e => e.IdPais)
                .HasColumnType("int(11)")
                .HasColumnName("IdPais");

            // Configuración de relaciones (asumiendo que las relaciones existen)
            builder.HasOne(cp => cp.Pais)
                .WithMany() // Asume que 'Pais' no tiene una colección de 'ContactoProspecto'
                .HasForeignKey(cp => cp.IdPais)
                .HasConstraintName("FK_contactoprospectos_pais");

            builder.HasOne(cp => cp.TipoContactoProspecto)
                .WithMany() // Asume que 'TipoContactoProspecto' no tiene una colección de 'ContactoProspecto'
                .HasForeignKey(cp => cp.IdTipo)
                .HasConstraintName("FK_contactoprospectos_tipo_contacto");

            // Configuración de la relación con Prospectos (si corresponde)
            builder.HasMany(cp => cp.Prospectos)
                .WithOne(p => p.Contacto)
                .HasForeignKey(p => p.IdContacto)
                .HasConstraintName("FK_prospecto_contacto");
        }
    }
}
