﻿using look.domain.entities.admin;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
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
        public DbSet<Email> Email { get; set; }
        public DbSet<TipoEmail> TipoEmail { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Telefono> Telefono { get; set; }
        public DbSet<TipoTelefono> TipoTelefono { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<TipoDireccion> TipoDireccion { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<EstadoProyecto> EstadoProyecto { get; set; }
        public DbSet<EstadoProspecto> EstadoProspecto { get; set; }
        public DbSet<EstadoPropuesta> EstadoPropuesta { get; set; }
        public DbSet<Prospecto> Prospecto { get; set; }
        public DbSet<TipoServicio> TipoServicios { get; set; }

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

                entity.HasOne(d => d.Persona).WithMany()
                    .HasForeignKey(d => d.PerId)
                    .HasConstraintName("FK_Usuario_perfil");

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
            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.EmaId).HasName("PRIMARY");

                entity.ToTable("email");

                entity.HasIndex(e => e.CliId, "FK_Email_Cliente");

                entity.HasIndex(e => e.PerId, "FK_Email_Persona");

                entity.HasIndex(e => e.TemId, "FK_Email_Tipo_Email");

                entity.Property(e => e.EmaId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("ema_id");
                entity.Property(e => e.CliId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cli_id");
                entity.Property(e => e.EmaEmail)
                    .HasMaxLength(100)
                    .HasColumnName("ema_email");
                entity.Property(e => e.EmaVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("ema_vigente");
                entity.Property(e => e.PerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("per_id");
                entity.Property(e => e.TemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tem_id");
                entity.Property(e => e.EmaPrincipal)
                  .HasColumnType("int(11)")
                  .HasColumnName("ema_principal");

                entity.HasOne(d => d.Cli).WithMany()
                    .HasForeignKey(d => d.CliId)
                    .HasConstraintName("FK_Email_Cliente");

                entity.HasOne(d => d.Per).WithMany()
                    .HasForeignKey(d => d.PerId)
                    .HasConstraintName("FK_Email_Persona");

                entity.HasOne(d => d.Tem).WithMany()
                    .HasForeignKey(d => d.TemId)
                    .HasConstraintName("FK_Email_Tipo_Email");
            });

            modelBuilder.Entity<TipoEmail>(entity =>
            {
                entity.HasKey(e => e.temId).HasName("PRIMARY");
                entity.ToTable("tipo_email");
                entity.Property(e => e.temId)
                    .ValueGeneratedNever()
                    .HasColumnType("int")
                    .HasColumnName("tem_id");
                entity.Property(e => e.temNombre)
                    .HasMaxLength(50)
                    .HasColumnName("tem_nombre");
                entity.Property(e => e.temVigente)
                    .HasColumnType("tinyint")
                    .HasColumnName("tem_vigente");
            });
            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasKey(e => e.telId).HasName("PRIMARY");
                entity.ToTable("telefono");
                entity.HasIndex(e => e.cliId, "FK_Telefono_Cliente");
                entity.HasIndex(e => e.perId, "FK_Telefono_Persona");
                entity.HasIndex(e => e.tteId, "FK_Telefono_Tipo_Telefono");
                entity.Property(e => e.telId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("tel_id");
                entity.Property(e => e.cliId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cli_id");
                entity.Property(e => e.telNumero)
                    .HasMaxLength(100)
                    .HasColumnName("tel_numero");
                entity.Property(e => e.telVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("tel_vigente");
                entity.Property(e => e.perId)
                    .HasColumnType("int(11)")
                    .HasColumnName("per_id");
                entity.Property(e => e.tteId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tte_id");
                entity.Property(e => e.TelPrincipal)
                    .HasColumnType("int(11)")
                    .HasColumnName("tel_principal");
                entity.HasOne(d => d.cliente).WithMany()
                    .HasForeignKey(d => d.cliId)
                    .HasConstraintName("FK_Telefono_Cliente");
                entity.HasOne(d => d.persona).WithMany()
                    .HasForeignKey(d => d.perId)
                    .HasConstraintName("FK_Telefono_Persona");
                entity.HasOne(d => d.tipoTelefono).WithMany()
                    .HasForeignKey(d => d.tteId)
                    .HasConstraintName("FK_Telefono_Tipo_Telefono");
            });
            modelBuilder.Entity<TipoTelefono>(entity =>
            {
                entity.HasKey(e => e.tteId).HasName("PRIMARY");
                entity.ToTable("tipo_telefono");
                entity.Property(e => e.tteId)
                    .ValueGeneratedNever()
                    .HasColumnType("int")
                    .HasColumnName("tte_id");
                entity.Property(e => e.tteNombre)
                    .HasMaxLength(50)
                    .HasColumnName("tte_nombre");
                entity.Property(e => e.tteVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("tte_vigente");
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
                    .ValueGeneratedOnAdd()
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
                entity.Property(e => e.CliNif)
                   .HasMaxLength(100)
                   .HasColumnName("cli_nif");

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
                    entity.HasKey(e => e.MyRowId).HasName("PRIMARY");
                entity.ToTable("cliente_persona");

                entity.HasKey(e => e.MyRowId); 

                entity.HasIndex(e => e.CarId, "FK_cliente_persona_Car");
                entity.HasIndex(e => e.CliId, "FK_cliente_persona_Cliente");
                entity.HasIndex(e => e.PerId, "FK_cliente_persona_Persona");

                entity.Property(e => e.MyRowId)
                    .HasColumnType("int(11)") //  tipo de datos sea el mismo que en la base de datos
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

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
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.RolId).HasName("PRIMARY");

                entity.ToTable("rol");

                entity.Property(e => e.RolId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("rol_id");
                entity.Property(e => e.RolDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("rol_descripcion");
                entity.Property(e => e.RolNombre)
                    .HasMaxLength(50)
                    .HasColumnName("rol_nombre");
            });
            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.DirId).HasName("PRIMARY");

                entity.ToTable("direccion");

                entity.HasIndex(e => e.CliId, "FK_Direccion_Cliente");

                entity.HasIndex(e => e.PerId, "FK_Direccion_Persona");

                entity.HasIndex(e => e.TdiId, "FK_Direccion_Tipo_Direccion");
                
                entity.HasIndex(e => e.ComId, "FK_Direccion_Comuna");

                entity.Property(e => e.DirId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("dir_id");
                entity.Property(e => e.PerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("per_id");
                entity.Property(e => e.CliId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cli_id");
                entity.Property(e => e.DirCalle)
                    .HasMaxLength(50)
                    .HasColumnName("dir_calle");
                entity.Property(e => e.DirNumero)
                    .HasMaxLength(50)
                    .HasColumnName("dir_numero");
                entity.Property(e => e.ComId)
                    .HasColumnType("int(11)")
                    .HasColumnName("com_id");
                entity.Property(e => e.DirBlock)
                    .HasMaxLength(50)
                    .HasColumnName("dir_block");
                entity.Property(e => e.TdiId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tdi_id");
                entity.Property(e => e.DirPrincipal)
                 .HasColumnType("int(11)")
                 .HasColumnName("dir_principal");

                entity.HasOne(d => d.Cli).WithMany()
                    .HasForeignKey(d => d.CliId)
                    .HasConstraintName("FK_Direccion_Cliente");

                entity.HasOne(d => d.Per).WithMany()
                    .HasForeignKey(d => d.PerId)
                    .HasConstraintName("FK_Direccion_Persona");

                entity.HasOne(d => d.Tdi).WithMany()
                    .HasForeignKey(d => d.TdiId)
                    .HasConstraintName("FK_Direccion_Tipo_Direccion");
                
                entity.HasOne(d => d.Com).WithMany()
                    .HasForeignKey(d => d.ComId)
                    .HasConstraintName("FK_Direccion_Comuna");
            });

            modelBuilder.Entity<TipoDireccion>(entity =>
            {
                entity.HasKey(e => e.TdiId).HasName("PRIMARY");
                entity.ToTable("tipo_direccion");
                entity.Property(e => e.TdiId)
                    .ValueGeneratedNever()
                    .HasColumnType("int")
                    .HasColumnName("tdi_id");
                entity.Property(e => e.TdiNombre)
                    .HasMaxLength(50)
                    .HasColumnName("tdi_nombre");
                entity.Property(e => e.TdiVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("tdi_vigente");
            });


            modelBuilder.Entity<Moneda>(entity =>

            {

                entity.HasKey(e => e.MonId).HasName("PRIMARY");

                entity.ToTable("moneda");

                entity.HasIndex(e => e.PaiId, "FK_Moneda_Pais");

                entity.Property(e => e.MonId)

                    .ValueGeneratedNever()

                    .HasColumnType("int(11)")

                    .HasColumnName("mon_id");

                entity.Property(e => e.MonNombre)

                    .HasColumnType("int(11)")

                    .HasColumnName("mon_nombre");

                entity.Property(e => e.MonVigente)

                    .HasColumnType("tinyint(4)")

                    .HasColumnName("mon_vigente");

                entity.Property(e => e.PaiId)

                    .HasColumnType("int(11)")

                    .HasColumnName("pai_id");

                entity.HasOne(d => d.Pais).WithMany()

                    .HasForeignKey(d => d.PaiId)

                    .HasConstraintName("FK_Moneda_Pais");

            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(e => e.DocId).HasName("PRIMARY");
                entity.ToTable("documento");
                entity.HasIndex(e => e.TdoId, "FK_Documento_Tipo_Documento");
                entity.HasIndex(e => e.DocIdCliente, "FK_Documento_Cliente");
                entity.Property(e => e.DocId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("doc_id");
                entity.Property(e => e.DocExtencion)
                    .HasMaxLength(50)
                    .HasColumnName("doc_extencion");
                entity.Property(e => e.DocNombre)
                    .HasMaxLength(50)
                    .HasColumnName("doc_nombre");
                entity.Property(e => e.DocUrl)
                    .HasMaxLength(50)
                    .HasColumnName("doc_url");
                entity.Property(e => e.TdoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tdo_id");

                entity.Property(e => e.DocIdCliente)
                   .HasColumnType("int(11)")
                   .HasColumnName("doc_id_cliente");

                entity.HasOne(d => d.TipoDoc).WithMany()
                    .HasForeignKey(d => d.TdoId)
                    .HasConstraintName("FK_Documento_Tipo_Documento");

                entity.HasOne(d => d.DocCli).WithMany()
                    .HasForeignKey(d => d.DocIdCliente)
                    .HasConstraintName("FK_Documento_Cliente");

            });
            
            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.TdoId).HasName("PRIMARY");
                entity.ToTable("tipo_documento");
                entity.Property(e => e.TdoId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("tdo_id");
                entity.Property(e => e.TdoDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("tdo_descripcion");
                entity.Property(e => e.TdoNombre)
                    .HasMaxLength(50)
                    .HasColumnName("tdo_nombre");

            });

            modelBuilder.Entity<EstadoProyecto>(entity =>
            {
                entity.HasKey(e => e.EpyId).HasName("PRIMARY");
                entity.ToTable("estado_proyecto");
                entity.Property(e => e.EpyId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("epy_id");
                entity.Property(e => e.EpyDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("epy_descripcion");
                entity.Property(e => e.EpyNombre)
                    .HasMaxLength(50)
                    .HasColumnName("epy_nombre");

            });

            modelBuilder.Entity<EstadoProspecto>(entity =>
            {
                entity.HasKey(e => e.EpsId).HasName("PRIMARY");
                entity.ToTable("estado_prospecto");
                entity.Property(e => e.EpsId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("eps_id");
                entity.Property(e => e.EpsDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("eps_descripcion");               

            });

            modelBuilder.Entity<EstadoPropuesta>(entity =>
            {
                entity.HasKey(e => e.EppId).HasName("PRIMARY");
                entity.ToTable("estado_propuesta");
                entity.Property(e => e.EppId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("epp_id");
                entity.Property(e => e.EppDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("epp_descripcion");

            });
         

              modelBuilder.Entity<Prospecto>(entity =>
              {

                  entity.HasKey(e => e.PrsId).HasName("PRIMARY");
                  entity.ToTable("prospecto");
                  entity.HasIndex(e => e.CliId, "FK_Proespecto_Cliente");
                  entity.HasIndex(e => e.EpsId, "FK_Proespecto_Estado_Prospecto");
                  entity.HasIndex(e => e.MonId, "FK_Proespecto_Moneda");
                  entity.HasIndex(e => e.TseId, "FK_Proespecto_Tipo_Servicio");
                  entity.Property(e => e.PrsId)
                      .ValueGeneratedNever()
                      .HasColumnType("int(11)")
                      .HasColumnName("prs_id");
                  entity.Property(e => e.CliId)
                      .HasColumnType("int(11)")
                      .HasColumnName("cli_id");
                  entity.Property(e => e.EpsId)
                      .HasColumnType("int(11)")
                      .HasColumnName("eps_id");
                  entity.Property(e => e.MonId)
                      .HasColumnType("int(11)")
                      .HasColumnName("mon_id");
                  entity.Property(e => e.PrsDescripcion)
                      .HasColumnType("varchar(50)")
                      .HasColumnName("prs_descripcion");
                  entity.Property(e => e.PrsFechaInicio)
                      .HasColumnType("datetime")
                      .HasColumnName("prs_fechainicio");
                  entity.Property(e => e.PrsPresupuesto)
                      .HasPrecision(18, 2)
                      .HasColumnName("prs_presupuesto");
                  entity.Property(e => e.TseId)
                      .HasColumnType("int(11)")
                      .HasColumnName("tse_id");
                  entity.HasOne(d => d.Cli).WithMany()
                      .HasForeignKey(d => d.CliId)
                      .HasConstraintName("FK_Proespecto_Cliente");
                  entity.HasOne(d => d.Esps).WithMany()
                      .HasForeignKey(d => d.EpsId)
                      .HasConstraintName("FK_Proespecto_Estado_Prospecto");
                  entity.HasOne(d => d.Mon).WithMany()
                      .HasForeignKey(d => d.MonId)
                      .HasConstraintName("FK_Proespecto_Moneda");
                  entity.HasOne(d => d.TipSer).WithMany()
                      .HasForeignKey(d => d.TseId)
                      .HasConstraintName("FK_Proespecto_Tipo_Servicio");

              });
            modelBuilder.Entity<TipoServicio>(entity =>
            {
                entity.HasKey(e => e.TseId).HasName("PRIMARY");

                entity.ToTable("tipo_servicio");

                entity.Property(e => e.TseId)
                    .ValueGeneratedNever()
                    .HasColumnType("int(11)")
                    .HasColumnName("tse_id");
                entity.Property(e => e.TseDescripcion)
                    .HasMaxLength(50)
                    .HasColumnName("tse_descripcion");
                entity.Property(e => e.TseNombre)
                    .HasMaxLength(50)
                    .HasColumnName("tse_nombre");
                entity.Property(e => e.TseVigente)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("tse_vigente");
            });

        }

    }
}