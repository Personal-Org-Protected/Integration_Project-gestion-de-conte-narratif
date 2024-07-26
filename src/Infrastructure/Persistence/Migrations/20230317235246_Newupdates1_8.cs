using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class Newupdates1_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Histoire_StoryTellingIdStoryTelling",
                table: "BasketItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_StoryTellingIdStoryTelling",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "item_id",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "StoryTellingIdStoryTelling",
                table: "BasketItems",
                newName: "IdStoryTelling");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems",
                columns: new[] { "IdStoryTelling", "basket_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Histoire_IdStoryTelling",
                table: "BasketItems",
                column: "IdStoryTelling",
                principalTable: "Histoire",
                principalColumn: "IdStoryTelling",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Histoire_IdStoryTelling",
                table: "BasketItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "IdStoryTelling",
                table: "BasketItems",
                newName: "StoryTellingIdStoryTelling");

            migrationBuilder.AddColumn<int>(
                name: "item_id",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems",
                columns: new[] { "item_id", "basket_id" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_StoryTellingIdStoryTelling",
                table: "BasketItems",
                column: "StoryTellingIdStoryTelling");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Histoire_StoryTellingIdStoryTelling",
                table: "BasketItems",
                column: "StoryTellingIdStoryTelling",
                principalTable: "Histoire",
                principalColumn: "IdStoryTelling",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
