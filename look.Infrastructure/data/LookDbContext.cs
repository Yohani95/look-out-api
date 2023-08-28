using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.entities.world;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data
{
    public class LookDbContext : DbContext
    {
        public LookDbContext(DbContextOptions<LookDbContext> options) : base(options)
        {

        }
        public DbSet<Usuario>? Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Lenguaje> Lenguaje { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Region>? Region { get; set; }
        public DbSet<TipoPersona> TipoPersona { get; set; }
        public DbSet<EstadoCliente>? EstadoCliente { get; set; }
        public DbSet<Giro>? Giro { get; set; }
        public DbSet<SectorComercial> SectorComercials { get; set; }
        public DbSet<Cliente>? Cliente { get; set; }
        public DbSet<Comuna>? Comuna { get; set; }
        public DbSet<ClientePersona> ClientePersona { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuId).HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.PerId, "FK_Usuario_Persona");

                entity.Property(e => e.UsuId)
                    .HasColumnType("int(11)")
                    .HasColumnName("usu_id");
                entity.Property(e => e.PerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("per_id");
                entity.Property(e => e.PrfId)
                    .HasColumnType("int(11)")
                    .HasColumnName("prf_id");
                entity.Property(e => e.UsuContraseña)
                    .HasMaxLength(50)
                    .HasColumnName("usu_contraseña");
                entity.Property(e => e.UsuNombre)
                    .HasMaxLength(50)
                    .HasColumnName("usu_nombre");
                entity.Property(e => e.UsuVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("usu_vigente");

                //entity.HasOne(d => d.Persona).WithMany()
                //    .HasForeignKey(d => d.PerId)
                //    .HasConstraintName("FK_Usuario_perfil");

                entity.HasOne(d => d.Perfil).WithMany()
                    .HasForeignKey(d => d.PerId)
                    .HasConstraintName("FK_Usuario_Persona");
            });
            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.PaiId).HasName("PRIMARY");

                entity.ToTable("pais");

                entity.HasIndex(e => e.LenId, "FK_pais_id_Lenguaje");

                entity.Property(e => e.PaiId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("pai_id");
                entity.Property(e => e.LenId)
                    .HasColumnType("int(11)")
                    .HasColumnName("len_id");
                entity.Property(e => e.PaiNombre)
                    .HasMaxLength(50)
                    .HasColumnName("pai_nombre");

                entity.HasOne(d => d.Lenguaje).WithMany()
                    .HasForeignKey(d => d.LenId)
                    .HasConstraintName("FK_pais_id_Lenguaje");
            });
            modelBuilder.Entity<Lenguaje>(entity =>
            {
                entity.HasKey(e => e.LenId).HasName("PRIMARY");

                entity.ToTable("lenguaje");

                entity.Property(e => e.LenId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("len_id");
                entity.Property(e => e.LenNombre)
                    .HasMaxLength(50)
                    .HasColumnName("len_nombre");
                entity.Property(e => e.LenVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("len_vigente");
            });
            modelBuilder.Entity<TipoPersona>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("tipo_persona");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.TpeDescripcion)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("tpe_descripcion")
                    .UseCollation("utf8_general_ci");
            });
            modelBuilder.Entity<Persona>(entity =>
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

                entity.HasOne(d => d.TipoPersona).WithMany()
                    .HasForeignKey(d => d.TpeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Tipo_Persona");
            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CliId).HasName("PRIMARY");

                entity.ToTable("cliente");

                entity.HasIndex(e => e.GirId, "FK_Cliente_Giro");

                entity.HasIndex(e => e.SecId, "FK_Cliente_Sector_Comercial");

                entity.HasIndex(e => e.PaiId, "FK_Cliente_pais_id");

                entity.HasIndex(e => e.EclId, "FK_ecl_cl");

                entity.Property(e => e.CliId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("cli_id");
                entity.Property(e => e.CliDescripcion)
                    .HasMaxLength(100)
                    .HasColumnName("cli_descripcion");
                entity.Property(e => e.CliNombre)
                    .HasMaxLength(100)
                    .HasColumnName("cli_nombre");
                entity.Property(e => e.CliSitioWeb)
                    .HasMaxLength(100)
                    .HasColumnName("cli_sitio_web");
                entity.Property(e => e.EclId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ecl_id");
                entity.Property(e => e.GirId)
                    .HasColumnType("int(11)")
                    .HasColumnName("gir_id");
                entity.Property(e => e.PaiId)
                    .HasColumnType("int(11)")
                    .HasColumnName("pai_id");
                entity.Property(e => e.SecId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sec_id");

                entity.HasOne(d => d.EstadoCliente).WithMany()
                    .HasForeignKey(d => d.EclId)
                    .HasConstraintName("FK_ecl_cl");

                entity.HasOne(d => d.Giro).WithMany()
                    .HasForeignKey(d => d.GirId)
                    .HasConstraintName("FK_Cliente_Giro");

                entity.HasOne(d => d.Pais).WithMany()
                    .HasForeignKey(d => d.PaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_pais_id");

                entity.HasOne(d => d.SectorComercial).WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.SecId)
                    .HasConstraintName("FK_Cliente_Sector_Comercial");
            });
            modelBuilder.Entity<Giro>(entity =>
            {
                entity.HasKey(e => e.GirId).HasName("PRIMARY");

                entity.ToTable("giro");

                entity.Property(e => e.GirId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("gir_id");
                entity.Property(e => e.GirDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("gir_descripcion");
                entity.Property(e => e.GirNombre)
                    .HasMaxLength(50)
                    .HasColumnName("gir_nombre");
                entity.Property(e => e.GirVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("gir_vigente");
            });
            modelBuilder.Entity<EstadoCliente>(entity =>
            {
                entity.HasKey(e => e.EclId).HasName("PRIMARY");

                entity.ToTable("estado_cliente");

                entity.Property(e => e.EclId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("ecl_id");
                entity.Property(e => e.EclNombre)
                    .HasMaxLength(50)
                    .HasColumnName("ecl_nombre");
            });
            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.HasKey(e => e.PrvId).HasName("PRIMARY");

                entity.ToTable("provincia");

                entity.HasIndex(e => e.RegId, "FK_Provincia_Region");

                entity.Property(e => e.PrvId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("prv_id");
                entity.Property(e => e.PrvNombre)
                    .HasMaxLength(50)
                    .HasColumnName("prv_nombre");
                entity.Property(e => e.RegId)
                    .HasColumnType("int(11)")
                    .HasColumnName("reg_id");

                entity.HasOne(d => d.Reg).WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.RegId)
                    .HasConstraintName("FK_Provincia_Region");
            });
            modelBuilder.Entity<Comuna>(entity =>
            {
                entity.HasKey(e => e.ComId).HasName("PRIMARY");

                entity.ToTable("comuna");

                entity.HasIndex(e => e.PrvId, "FK_Comuna_Provincia");

                entity.Property(e => e.ComId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("com_id");
                entity.Property(e => e.ComNombre)
                    .HasMaxLength(50)
                    .HasColumnName("com_nombre");
                entity.Property(e => e.PrvId)
                    .HasColumnType("int(11)")
                    .HasColumnName("prv_id");

                entity.HasOne(d => d.Prv).WithMany(p => p.Comunas)
                    .HasForeignKey(d => d.PrvId)
                    .HasConstraintName("FK_Comuna_Provincia");
            });
            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegId).HasName("PRIMARY");

                entity.ToTable("region");

                entity.HasIndex(e => e.PaiId, "FK_Region_pais_id");

                entity.Property(e => e.RegId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("reg_id");
                entity.Property(e => e.PaiId)
                    .HasColumnType("int(11)")
                    .HasColumnName("pai_id");
                entity.Property(e => e.RegNombre)
                    .HasMaxLength(50)
                    .HasColumnName("reg_nombre");
                entity.Property(e => e.RegNumero)
                    .HasColumnType("int(11)")
                    .HasColumnName("reg_numero");

                entity.HasOne(d => d.Pais).WithMany()
                    .HasForeignKey(d => d.PaiId)
                    .HasConstraintName("FK_Region_pais_id");
            });
            modelBuilder.Entity<SectorComercial>(entity =>
            {
                entity.HasKey(e => e.SecId).HasName("PRIMARY");

                entity.ToTable("sector_comercial");

                entity.Property(e => e.SecId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("sec_id");
                entity.Property(e => e.SecDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("sec_descripcion");
                entity.Property(e => e.SecNombre)
                    .HasMaxLength(50)
                    .HasColumnName("sec_nombre");
                entity.Property(e => e.SecVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("sec_vigente");
            });
            modelBuilder.Entity<ClientePersona>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("cliente_persona");

                entity.HasIndex(e => e.CarId, "FK_cliente_persona_Car");

                entity.HasIndex(e => e.CliId, "FK_cliente_persona_Cliente");

                entity.HasIndex(e => e.PerId, "FK_cliente_persona_Persona");

                entity.Property(e => e.CarId)
                    .HasColumnType("int(11)")
                    .HasColumnName("car_id");
                entity.Property(e => e.CliId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cli_id");
                entity.Property(e => e.CliVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("cli_vigente");
                entity.Property(e => e.PerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("per_id");

                //entity.HasOne(d => d.Car).WithMany()
                //    .HasForeignKey(d => d.CarId)
                //    .HasConstraintName("FK_cliente_persona_Car");

                entity.HasOne(d => d.Cliente).WithMany()
                    .HasForeignKey(d => d.CliId)
                    .HasConstraintName("FK_cliente_persona_Cliente");

                entity.HasOne(d => d.Persona).WithMany()
                    .HasForeignKey(d => d.PerId)
                    .HasConstraintName("FK_cliente_persona_Persona");
            });


        }

    }
}
