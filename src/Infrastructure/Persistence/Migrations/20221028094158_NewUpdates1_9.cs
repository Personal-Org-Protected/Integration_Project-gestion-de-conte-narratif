using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates1_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern");

            migrationBuilder.DropIndex(
                name: "IX_UserIntern_IdForfait",
                table: "UserIntern");

            migrationBuilder.DropColumn(
                name: "IdForfait",
                table: "UserIntern");

            migrationBuilder.CreateTable(
                name: "Forfait_User",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdForfait = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forfait_User", x => new { x.IdUser, x.IdForfait });
                    table.ForeignKey(
                        name: "FK_Forfait_User_Forfait_IdForfait",
                        column: x => x.IdForfait,
                        principalTable: "Forfait",
                        principalColumn: "IdForfait",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forfait_User_UserIntern_IdUser",
                        column: x => x.IdUser,
                        principalTable: "UserIntern",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forfait_User_IdForfait",
                table: "Forfait_User",
                column: "IdForfait");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forfait_User");

            migrationBuilder.AddColumn<int>(
                name: "IdForfait",
                table: "UserIntern",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserIntern_IdForfait",
                table: "UserIntern",
                column: "IdForfait");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIntern_Forfait_IdForfait",
                table: "UserIntern",
                column: "IdForfait",
                principalTable: "Forfait",
                principalColumn: "IdForfait");
        }
    }
}
