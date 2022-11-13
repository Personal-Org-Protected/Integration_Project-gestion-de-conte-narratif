using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates1_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Story_FK",
                table: "Chapitre");

            migrationBuilder.DropIndex(
                name: "IX_Chapitre_Idstory",
                table: "Chapitre");

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_Idstory",
                table: "Chapitre",
                column: "Idstory",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Story_FK",
                table: "Chapitre",
                column: "Idstory",
                principalTable: "Story",
                principalColumn: "IdStory",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Story_FK",
                table: "Chapitre");

            migrationBuilder.DropIndex(
                name: "IX_Chapitre_Idstory",
                table: "Chapitre");

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_Idstory",
                table: "Chapitre",
                column: "Idstory");

            migrationBuilder.AddForeignKey(
                name: "Story_FK",
                table: "Chapitre",
                column: "Idstory",
                principalTable: "Story",
                principalColumn: "IdStory");
        }
    }
}
