using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates1_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdLibrary",
                table: "Box d'Histoire",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Box d'Histoire_IdLibrary",
                table: "Box d'Histoire",
                column: "IdLibrary");

            migrationBuilder.AddForeignKey(
                name: "Library_ref",
                table: "Box d'Histoire",
                column: "IdLibrary",
                principalTable: "Bibliotheque",
                principalColumn: "IdLibrary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Library_ref",
                table: "Box d'Histoire");

            migrationBuilder.DropIndex(
                name: "IX_Box d'Histoire_IdLibrary",
                table: "Box d'Histoire");

            migrationBuilder.DropColumn(
                name: "IdLibrary",
                table: "Box d'Histoire");
        }
    }
}
