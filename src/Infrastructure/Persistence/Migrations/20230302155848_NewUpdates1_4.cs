using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class NewUpdates1_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Popularite",
                table: "Histoire",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    idNotification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datedenotif = table.Column<DateTime>(name: "date de notif", type: "date", nullable: false),
                    target = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.idNotification);
                    table.ForeignKey(
                        name: "FK_Notification_User_target",
                        column: x => x.target,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    basket_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Proprietaire = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Paniervide = table.Column<bool>(name: "Panier vide", type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.basket_id);
                    table.ForeignKey(
                        name: "FK_Panier_User_Proprietaire",
                        column: x => x.Proprietaire,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    basket_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    StoryTellingIdStoryTelling = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => new { x.item_id, x.basket_id });
                    table.ForeignKey(
                        name: "FK_BasketItems_Histoire_StoryTellingIdStoryTelling",
                        column: x => x.StoryTellingIdStoryTelling,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Panier_basket_id",
                        column: x => x.basket_id,
                        principalTable: "Panier",
                        principalColumn: "basket_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_basket_id",
                table: "BasketItems",
                column: "basket_id");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_StoryTellingIdStoryTelling",
                table: "BasketItems",
                column: "StoryTellingIdStoryTelling");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_target",
                table: "Notification",
                column: "target");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_Proprietaire",
                table: "Panier",
                column: "Proprietaire",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Panier");

            migrationBuilder.DropColumn(
                name: "avatar",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Popularite",
                table: "Histoire");
        }
    }
}
