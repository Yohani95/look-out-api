using look.domain.entities.proyectoDesarrollo;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.proyectoDesarrollo
{
    internal class EtapaPlanificacionProyectoDesarrolloConfiguration : IEntityTypeConfiguration<EtapaPlanificacionProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<EtapaPlanificacionProyectoDesarrollo> builder)
        {
            // Establecer el nombre de la tabla
            builder.ToTable("Etapa_Planificacion_Proyecto_Desarrollo");

            // Configurar clave primaria
            builder.HasKey(e => e.Id);

            // Configurar propiedades
            builder.Property(e => e.Nombre)
                   .HasMaxLength(200) // Limitar la longitud del campo
                   .IsRequired(false); // Permitir que sea opcional

            builder.Property(e => e.Descripcion)
                   .HasMaxLength(500) // Limitar la longitud del campo
                   .IsRequired(false); // Permitir que sea opcional
        }
    }
}
