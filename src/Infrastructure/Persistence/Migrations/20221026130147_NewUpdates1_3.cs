using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates1_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern",
                column: "IdForfait",
                principalTable: "Forfait",
                principalColumn: "IdForfait");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern",
                column: "IdForfait",
                principalTable: "Forfait",
                principalColumn: "IdForfait",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
