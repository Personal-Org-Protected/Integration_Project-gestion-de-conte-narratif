using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates1_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIntern_Forfait_Forfait reference",
                table: "UserIntern");

            migrationBuilder.DropIndex(
                name: "IX_UserIntern_Forfait reference",
                table: "UserIntern");

            migrationBuilder.RenameColumn(
                name: "Forfait reference",
                table: "UserIntern",
                newName: "IdForfait");

            migrationBuilder.CreateIndex(
                name: "IX_UserIntern_IdForfait",
                table: "UserIntern",
                column: "IdForfait");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern",
                column: "IdForfait",
                principalTable: "Forfait",
                principalColumn: "IdForfait",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern");

            migrationBuilder.DropIndex(
                name: "IX_UserIntern_IdForfait",
                table: "UserIntern");

            migrationBuilder.RenameColumn(
                name: "IdForfait",
                table: "UserIntern",
                newName: "Forfait reference");

            migrationBuilder.CreateIndex(
                name: "IX_UserIntern_Forfait reference",
                table: "UserIntern",
                column: "Forfait reference",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserIntern_Forfait_Forfait reference",
                table: "UserIntern",
                column: "Forfait reference",
                principalTable: "Forfait",
                principalColumn: "IdForfait",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
