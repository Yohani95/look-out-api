using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace look.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnaEstadoPropecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estado_prospecto");

            migrationBuilder.AddColumn<int>(
                name: "idEstadoProspecto",
                table: "prospecto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstadoProspecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoProspecto", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_idEstadoProspecto",
                table: "prospecto",
                column: "idEstadoProspecto");

            migrationBuilder.AddForeignKey(
                name: "FK_prospecto_estado_prospecto",
                table: "prospecto",
                column: "idEstadoProspecto",
                principalTable: "EstadoProspecto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prospecto_estado_prospecto",
                table: "prospecto");

            migrationBuilder.DropTable(
                name: "EstadoProspecto");

            migrationBuilder.DropIndex(
                name: "IX_prospecto_idEstadoProspecto",
                table: "prospecto");

            migrationBuilder.DropColumn(
                name: "idEstadoProspecto",
                table: "prospecto");

            migrationBuilder.CreateTable(
                name: "estado_prospecto",
                columns: table => new
                {
                    eps_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    eps_descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.eps_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
