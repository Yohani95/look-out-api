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
    internal class ProyectoDesarrolloConfiguration : IEntityTypeConfiguration<ProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<ProyectoDesarrollo> builder)
        {
            // Establecer el nombre de la tabla
            builder.ToTable("proyecto_desarrollo");

            // Configuración de la clave primaria
            builder.HasKey(p => p.Id).HasName("PRIMARY");

            // Configuración de las propiedades
            builder.Property(p => p.Id)
                   .HasColumnType("int(11)")
                   .HasColumnName("id");

            builder.Property(p => p.Nombre)
                   .HasColumnType("varchar(200)")
                   .HasColumnName("nombre")
                   .IsRequired(false);

            builder.Property(p => p.FechaCierre)
                   .HasColumnType("date")
                   .HasColumnName("fecha_cierre")
                   .IsRequired(false);

            builder.Property(p => p.avance)
                   .HasColumnType("double")
                   .HasColumnName("avance")
                   .IsRequired(false);

            builder.Property(p => p.IdCliente)
                   .HasColumnType("int")
                   .HasColumnName("id_cliente")
                   .IsRequired(false);

            builder.Property(p => p.IdContacto)
                   .HasColumnType("int")
                   .HasColumnName("id_contacto")
                   .IsRequired(false);

            builder.Property(p => p.IdKam)
                   .HasColumnType("int")
                   .HasColumnName("id_kam")
                   .IsRequired(false);

            builder.Property(p => p.IdMoneda)
                   .HasColumnType("int")
                   .HasColumnName("id_moneda")
                   .IsRequired(false);

            builder.Property(p => p.IdTipoProyecto)
                   .HasColumnType("int")
                   .HasColumnName("id_tipo_proyecto")
                   .IsRequired(false);

            builder.Property(p => p.IdEstado)
                   .HasColumnType("int")
                   .HasColumnName("id_estado")
                   .IsRequired(false);

            builder.Property(p => p.IdEtapa)
                   .HasColumnType("int")
                   .HasColumnName("id_etapa")
                   .IsRequired(false);

            builder.Property(p => p.IdPais)
                   .HasColumnType("int")
                   .HasColumnName("id_pais")
                   .IsRequired(false);

            builder.Property(p => p.IdEmpresaPrestadora)
                   .HasColumnType("int")
                   .HasColumnName("id_empresa_prestadora")
                   .IsRequired(false);

            builder.Property(p => p.IdJefeProyecto)
                   .HasColumnType("int")
                   .HasColumnName("id_jefe_proyecto")
                   .IsRequired(false);

            builder.Property(p => p.Monto)
                   .HasColumnType("double")
                   .HasColumnName("monto")
                   .IsRequired(false);

            builder.Property(e => e.FechaCreacion)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_creacion")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Configuración de las relaciones y claves foráneas
            builder.HasOne(p => p.Kam)
                   .WithMany()
                   .HasForeignKey(p => p.IdKam)
                   .HasConstraintName("FK_proyecto_desarrollo_kam");

            builder.HasOne(p => p.Cliente)
                   .WithMany()
                   .HasForeignKey(p => p.IdCliente)
                   .HasConstraintName("FK_proyecto_desarrollo_cliente");

            builder.HasOne(p => p.Estado)
                   .WithMany()
                   .HasForeignKey(p => p.IdEstado)
                   .HasConstraintName("FK_proyecto_desarrollo_estado");

            builder.HasOne(p => p.Moneda)
                   .WithMany()
                   .HasForeignKey(p => p.IdMoneda)
                   .HasConstraintName("FK_proyecto_desarrollo_moneda");

            builder.HasOne(p => p.Etapa)
                   .WithMany()
                   .HasForeignKey(p => p.IdEtapa)
                   .HasConstraintName("FK_proyecto_desarrollo_etapa");

            builder.HasOne(p => p.TipoProyectoDesarrollo)
                   .WithMany()
                   .HasForeignKey(p => p.IdTipoProyecto)
                   .HasConstraintName("FK_proyecto_desarrollo_tipo_proyecto");

            builder.HasOne(p => p.EmpresaPrestadora)
                   .WithMany()
                   .HasForeignKey(p => p.IdEmpresaPrestadora)
                   .HasConstraintName("FK_proyecto_desarrollo_empresa");

            builder.HasOne(p => p.JefeProyecto)
               .WithMany()
               .HasForeignKey(p => p.IdJefeProyecto)
               .HasConstraintName("FK_proyecto_jefe_proyecto");
        }
    }
}
