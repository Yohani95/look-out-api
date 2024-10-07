using look.domain.entities.proyectoDesarrollo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.proyectoDesarrollo
{
    internal class TipoProyectoDesarrolloConfiguration : IEntityTypeConfiguration<TipoProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<TipoProyectoDesarrollo> builder)
        {
            // Establecer el nombre de la tabla
            builder.ToTable("Tipo_Proyecto_Desarrollo");

            // Configurar clave primaria
            builder.HasKey(t => t.Id);

            // Configurar propiedades
            builder.Property(t => t.Nombre)
                   .HasMaxLength(200)  // Limitar la longitud del campo
                   .IsRequired(false); // Permitir que sea opcional

            builder.Property(t => t.Descripcion)
                   .HasMaxLength(500)  // Limitar la longitud del campo
                   .IsRequired(false); // Permitir que sea opcional

        }
    }
}
