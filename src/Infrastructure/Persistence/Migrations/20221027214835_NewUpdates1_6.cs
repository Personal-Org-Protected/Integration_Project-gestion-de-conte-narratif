using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates1_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Box de Bibliotheque_Histoire_IdStoryTell",
                table: "Box de Bibliotheque");

            migrationBuilder.DropTable(
                name: "Library_BoxStories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Box de Bibliotheque",
                table: "Box de Bibliotheque");

            migrationBuilder.RenameTable(
                name: "Box de Bibliotheque",
                newName: "Box d'Histoire");

            migrationBuilder.RenameIndex(
                name: "IX_Box de Bibliotheque_IdStoryTell",
                table: "Box d'Histoire",
                newName: "IX_Box d'Histoire_IdStoryTell");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Box d'Histoire",
                table: "Box d'Histoire",
                column: "IdBox");

            migrationBuilder.AddForeignKey(
                name: "FK_Box d'Histoire_Histoire_IdStoryTell",
                table: "Box d'Histoire",
                column: "IdStoryTell",
                principalTable: "Histoire",
                principalColumn: "IdStoryTelling");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Box d'Histoire_Histoire_IdStoryTell",
                table: "Box d'Histoire");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Box d'Histoire",
                table: "Box d'Histoire");

            migrationBuilder.RenameTable(
                name: "Box d'Histoire",
                newName: "Box de Bibliotheque");

            migrationBuilder.RenameIndex(
                name: "IX_Box d'Histoire_IdStoryTell",
                table: "Box de Bibliotheque",
                newName: "IX_Box de Bibliotheque_IdStoryTell");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Box de Bibliotheque",
                table: "Box de Bibliotheque",
                column: "IdBox");

            migrationBuilder.CreateTable(
                name: "Library_BoxStories",
                columns: table => new
                {
                    IdStoryTellBox = table.Column<int>(type: "int", nullable: false),
                    IdLibrary = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library_BoxStories", x => new { x.IdStoryTellBox, x.IdLibrary });
                    table.ForeignKey(
                        name: "FK_Library_BoxStories_Bibliotheque_IdLibrary",
                        column: x => x.IdLibrary,
                        principalTable: "Bibliotheque",
                        principalColumn: "IdLibrary",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Library_BoxStories_Box de Bibliotheque_IdStoryTellBox",
                        column: x => x.IdStoryTellBox,
                        principalTable: "Box de Bibliotheque",
                        principalColumn: "IdBox",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Library_BoxStories_IdLibrary",
                table: "Library_BoxStories",
                column: "IdLibrary");

            migrationBuilder.AddForeignKey(
                name: "FK_Box de Bibliotheque_Histoire_IdStoryTell",
                table: "Box de Bibliotheque",
                column: "IdStoryTell",
                principalTable: "Histoire",
                principalColumn: "IdStoryTelling");
        }
    }
}
