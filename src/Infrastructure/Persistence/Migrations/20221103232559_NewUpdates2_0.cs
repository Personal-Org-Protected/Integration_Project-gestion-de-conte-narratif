using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates2_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histoire",
                table: "Commentaire");

            migrationBuilder.RenameColumn(
                name: "IdStoryTell",
                table: "Commentaire",
                newName: "Zone de Commenataire");

            migrationBuilder.RenameIndex(
                name: "IX_Commentaire_IdStoryTell",
                table: "Commentaire",
                newName: "IX_Commentaire_Zone de Commenataire");

            migrationBuilder.AddColumn<int>(
                name: "IdZone",
                table: "Histoire",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "image vignette",
                table: "Histoire",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Zone de Commentaire",
                columns: table => new
                {
                    IdZone = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone de Commentaire", x => x.IdZone);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histoire_IdZone",
                table: "Histoire",
                column: "IdZone",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Zone Comm",
                table: "Commentaire",
                column: "Zone de Commenataire",
                principalTable: "Zone de Commentaire",
                principalColumn: "IdZone",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Histoire_Zone de Commentaire_IdZone",
                table: "Histoire",
                column: "IdZone",
                principalTable: "Zone de Commentaire",
                principalColumn: "IdZone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Zone Comm",
                table: "Commentaire");

            migrationBuilder.DropForeignKey(
                name: "FK_Histoire_Zone de Commentaire_IdZone",
                table: "Histoire");

            migrationBuilder.DropTable(
                name: "Zone de Commentaire");

            migrationBuilder.DropIndex(
                name: "IX_Histoire_IdZone",
                table: "Histoire");

            migrationBuilder.DropColumn(
                name: "IdZone",
                table: "Histoire");

            migrationBuilder.DropColumn(
                name: "image vignette",
                table: "Histoire");

            migrationBuilder.RenameColumn(
                name: "Zone de Commenataire",
                table: "Commentaire",
                newName: "IdStoryTell");

            migrationBuilder.RenameIndex(
                name: "IX_Commentaire_Zone de Commenataire",
                table: "Commentaire",
                newName: "IX_Commentaire_IdStoryTell");

            migrationBuilder.AddForeignKey(
                name: "FK_Histoire",
                table: "Commentaire",
                column: "IdStoryTell",
                principalTable: "Histoire",
                principalColumn: "IdStoryTelling",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
