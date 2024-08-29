using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace look.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eliminarEntidadesProspecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prospecto_contacto_prospecto",
                table: "prospecto");

            migrationBuilder.DropForeignKey(
                name: "FK_prospecto_empresa",
                table: "prospecto");

            migrationBuilder.DropTable(
                name: "ContactosProspecto");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_prospecto_IdContactoProspecto",
                table: "prospecto");

            migrationBuilder.DropIndex(
                name: "IX_prospecto_IdEmpresa",
                table: "prospecto");

            migrationBuilder.DropColumn(
                name: "IdContactoProspecto",
                table: "prospecto");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "prospecto");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_creacion",
                table: "prospecto",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_actividad",
                table: "prospecto",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "prospecto",
                type: "int(11)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdCliente",
                table: "prospecto",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_prospecto_cliente",
                table: "prospecto",
                column: "IdCliente",
                principalTable: "cliente",
                principalColumn: "cli_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prospecto_cliente",
                table: "prospecto");

            migrationBuilder.DropIndex(
                name: "IX_prospecto_IdCliente",
                table: "prospecto");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "prospecto");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_creacion",
                table: "prospecto",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_actividad",
                table: "prospecto",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdContactoProspecto",
                table: "prospecto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "prospecto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactosProspecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreCompleto = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PerfilLinkedin = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactosProspecto", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdIndustria = table.Column<int>(type: "int", nullable: true),
                    Detalle = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdContactoProspecto",
                table: "prospecto",
                column: "IdContactoProspecto");

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdEmpresa",
                table: "prospecto",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_IdIndustria",
                table: "Empresas",
                column: "IdIndustria");

            migrationBuilder.AddForeignKey(
                name: "FK_prospecto_contacto_prospecto",
                table: "prospecto",
                column: "IdContactoProspecto",
                principalTable: "ContactosProspecto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_prospecto_empresa",
                table: "prospecto",
                column: "IdEmpresa",
                principalTable: "Empresas",
                principalColumn: "Id");
        }
    }
}
