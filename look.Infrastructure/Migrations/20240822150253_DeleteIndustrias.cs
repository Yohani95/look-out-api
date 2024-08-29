using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace look.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIndustrias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prospecto_Industrias_IdIndustria",
                table: "prospecto");

            migrationBuilder.DropTable(
                name: "Industrias");

            migrationBuilder.DropIndex(
                name: "IX_prospecto_IdIndustria",
                table: "prospecto");

            migrationBuilder.DropColumn(
                name: "IdIndustria",
                table: "prospecto");

            migrationBuilder.AddColumn<int>(
                name: "IdContacto",
                table: "prospecto",
                type: "int(11)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdContacto",
                table: "prospecto",
                column: "IdContacto");

            migrationBuilder.AddForeignKey(
                name: "FK_prospecto_persona_IdContacto",
                table: "prospecto",
                column: "IdContacto",
                principalTable: "persona",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prospecto_persona_IdContacto",
                table: "prospecto");

            migrationBuilder.DropIndex(
                name: "IX_prospecto_IdContacto",
                table: "prospecto");

            migrationBuilder.DropColumn(
                name: "IdContacto",
                table: "prospecto");

            migrationBuilder.AddColumn<int>(
                name: "IdIndustria",
                table: "prospecto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Industrias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Detalle = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industrias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_prospecto_IdIndustria",
                table: "prospecto",
                column: "IdIndustria");

            migrationBuilder.AddForeignKey(
                name: "FK_prospecto_Industrias_IdIndustria",
                table: "prospecto",
                column: "IdIndustria",
                principalTable: "Industrias",
                principalColumn: "Id");
        }
    }
}
