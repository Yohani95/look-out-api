using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace look.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIndustriaProspecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdIndustria",
                table: "prospecto",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prospecto_Industrias_IdIndustria",
                table: "prospecto");

            migrationBuilder.DropIndex(
                name: "IX_prospecto_IdIndustria",
                table: "prospecto");

            migrationBuilder.DropColumn(
                name: "IdIndustria",
                table: "prospecto");
        }
    }
}
