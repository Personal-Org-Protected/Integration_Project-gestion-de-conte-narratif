using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class change_user_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "User",
                newName: "Profile Image");

            migrationBuilder.AddColumn<string>(
                name: "Azure Id",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role of user",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "member");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Azure Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Role of user",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Profile Image",
                table: "User",
                newName: "avatar");
        }
    }
}
