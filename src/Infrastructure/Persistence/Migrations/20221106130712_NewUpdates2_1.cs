using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates2_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bibliotheque_idLibrary",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_idLibrary",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "idLibrary",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "LibraryIdLibrary",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LibraryIdLibrary",
                table: "Transactions",
                column: "LibraryIdLibrary");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bibliotheque_LibraryIdLibrary",
                table: "Transactions",
                column: "LibraryIdLibrary",
                principalTable: "Bibliotheque",
                principalColumn: "IdLibrary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bibliotheque_LibraryIdLibrary",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_LibraryIdLibrary",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LibraryIdLibrary",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "idLibrary",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idLibrary",
                table: "Transactions",
                column: "idLibrary");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bibliotheque_idLibrary",
                table: "Transactions",
                column: "idLibrary",
                principalTable: "Bibliotheque",
                principalColumn: "IdLibrary");
        }
    }
}
