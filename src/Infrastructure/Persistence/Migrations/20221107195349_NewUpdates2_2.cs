using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates2_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "Image",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "idTag",
                table: "Histoire",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "Chapitre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Image_owner",
                table: "Image",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_Histoire_idTag",
                table: "Histoire",
                column: "idTag");

            migrationBuilder.AddForeignKey(
                name: "FK_Histoire_Tag_idTag",
                table: "Histoire",
                column: "idTag",
                principalTable: "Tag",
                principalColumn: "IdTag");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_UserIntern_owner",
                table: "Image",
                column: "owner",
                principalTable: "UserIntern",
                principalColumn: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histoire_Tag_idTag",
                table: "Histoire");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_UserIntern_owner",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_owner",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Histoire_idTag",
                table: "Histoire");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "idTag",
                table: "Histoire");

            migrationBuilder.DropColumn(
                name: "order",
                table: "Chapitre");
        }
    }
}
