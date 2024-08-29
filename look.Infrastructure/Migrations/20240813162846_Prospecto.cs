using System;
using look.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace look.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Prospecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTableIfNotExists(
                name: "ContactosProspecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCompleto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PerfilLinkedin = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactosProspecto", x => x.Id);
                })
                ;

            migrationBuilder.CreateTableIfNotExists(
                name: "Industrias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Detalle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industrias", x => x.Id);
                })
                ;
            migrationBuilder.CreateTableIfNotExists(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Detalle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdIndustria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empresa_industria",
                        column: x => x.IdIndustria,
                        principalTable: "Industrias",
                        principalColumn: "Id");
                })
                ;


            migrationBuilder.CreateTableIfNotExists(
                name: "prospecto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_actividad = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    IdKam = table.Column<int>(type: "int(11)", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdContactoProspecto = table.Column<int>(type: "int", nullable: true),
                    Contactado = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CantidadLlamadas = table.Column<int>(type: "int", nullable: true),
                    Responde = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_prospecto_contacto_prospecto",
                        column: x => x.IdContactoProspecto,
                        principalTable: "ContactosProspecto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_prospecto_empresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_prospecto_kam",
                        column: x => x.IdKam,
                        principalTable: "persona",
                        principalColumn: "id");
                });
            migrationBuilder.CreateIndex(
       name: "IX_Empresas_IdIndustria",
       table: "Empresas",
       column: "IdIndustria");


            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdContactoProspecto",
                table: "prospecto",
                column: "IdContactoProspecto");

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdEmpresa",
                table: "prospecto",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdKam",
                table: "prospecto",
                column: "IdKam");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prospecto");

            migrationBuilder.DropTable(
                name: "ContactosProspecto");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Industrias");

        }

        private bool TableExists(MigrationBuilder migrationBuilder, string tableName)
        {
            var result = migrationBuilder.Sql($@"
        SELECT COUNT(*)
        FROM information_schema.tables
        WHERE table_schema = DATABASE() AND table_name = '{tableName}';", true);

            return result.ToString() == "1";
        }

    }
}
