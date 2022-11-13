using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class MigrationStoryTell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    IdStory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chapitre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FondduChapitre = table.Column<string>(name: "Fond du Chapitre", type: "nvarchar(max)", nullable: false),
                    DatedeCreation = table.Column<DateTime>(name: "Date de Creation", type: "date", nullable: false),
                    DatedeModification = table.Column<DateTime>(name: "Date de Modification", type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.IdStory);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    IdTag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.IdTag);
                });

            migrationBuilder.CreateTable(
                name: "Forfait",
                columns: table => new
                {
                    IdForfait = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomdeForfait = table.Column<string>(name: "nom de Forfait", type: "nvarchar(max)", nullable: false),
                    valeurdeforfait = table.Column<double>(name: "valeur de forfait", type: "float", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forfait", x => x.IdForfait);
                    table.ForeignKey(
                        name: "FK_Forfait_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "IdRole");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(name: "Birth Date", type: "datetime2", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                    table.ForeignKey(
                        name: "Role_FK",
                        column: x => x.Role,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    IdImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTag = table.Column<int>(type: "int", nullable: false),
                    DatedeCreation = table.Column<DateTime>(name: "Date de Creation", type: "date", nullable: false),
                    DatedeModification = table.Column<DateTime>(name: "Date de Modification", type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.IdImage);
                    table.ForeignKey(
                        name: "Tag_FK",
                        column: x => x.IdTag,
                        principalTable: "Tag",
                        principalColumn: "IdTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserIntern",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Forfaitreference = table.Column<int>(name: "Forfait reference", type: "int", nullable: false),
                    UserIdentity = table.Column<string>(name: "User Identity", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIntern", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_UserIntern_Forfait_Forfait reference",
                        column: x => x.Forfaitreference,
                        principalTable: "Forfait",
                        principalColumn: "IdForfait",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserIntern_User_User Identity",
                        column: x => x.UserIdentity,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bibliotheque",
                columns: table => new
                {
                    IdLibrary = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomdeBibliotheque = table.Column<string>(name: "Nom de Bibliotheque", type: "nvarchar(max)", nullable: false),
                    usersProperty = table.Column<string>(name: "users Property", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibliotheque", x => x.IdLibrary);
                    table.ForeignKey(
                        name: "FK_Bibliotheque_UserIntern_users Property",
                        column: x => x.usersProperty,
                        principalTable: "UserIntern",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "Histoire",
                columns: table => new
                {
                    IdStoryTelling = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomHistoire = table.Column<string>(name: "Nom Histoire", type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatedecreationdelHistoire = table.Column<DateTime>(name: "Date de creation de l'Histoire", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histoire", x => x.IdStoryTelling);
                    table.ForeignKey(
                        name: "FK_Histoire_UserIntern_Owner",
                        column: x => x.Owner,
                        principalTable: "UserIntern",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Box de Bibliotheque",
                columns: table => new
                {
                    IdBox = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dernierchapitrelu = table.Column<int>(name: "Dernier chapitre lu", type: "int", nullable: false),
                    IdStoryTell = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box de Bibliotheque", x => x.IdBox);
                    table.ForeignKey(
                        name: "FK_Box de Bibliotheque_Histoire_IdStoryTell",
                        column: x => x.IdStoryTell,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling");
                });

            migrationBuilder.CreateTable(
                name: "Chapitre",
                columns: table => new
                {
                    IdChapitre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idimage = table.Column<int>(type: "int", nullable: false),
                    Idstory = table.Column<int>(type: "int", nullable: false),
                    IdstoryTelling = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapitre", x => x.IdChapitre);
                    table.ForeignKey(
                        name: "Histoire_FK",
                        column: x => x.IdstoryTelling,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling");
                    table.ForeignKey(
                        name: "Image_FK",
                        column: x => x.Idimage,
                        principalTable: "Image",
                        principalColumn: "IdImage");
                    table.ForeignKey(
                        name: "Story_FK",
                        column: x => x.Idstory,
                        principalTable: "Story",
                        principalColumn: "IdStory");
                });

            migrationBuilder.CreateTable(
                name: "Commentaire",
                columns: table => new
                {
                    IdCommentaire = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signalement = table.Column<int>(type: "int", nullable: false),
                    commentaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdStoryTell = table.Column<int>(type: "int", nullable: false),
                    Datedecreation = table.Column<DateTime>(name: "Date de creation", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaire", x => x.IdCommentaire);
                    table.ForeignKey(
                        name: "FK_Histoire",
                        column: x => x.IdStoryTell,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idees",
                columns: table => new
                {
                    IdIdee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdStoryTell = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idees", x => x.IdIdee);
                    table.ForeignKey(
                        name: "History_FK",
                        column: x => x.IdStoryTell,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nomdulivre = table.Column<string>(name: "Nom du livre", type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    Datedelatransaction = table.Column<DateTime>(name: "Date de la transaction", type: "datetime2", nullable: false),
                    idLibrary = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdStory = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Bibliotheque_idLibrary",
                        column: x => x.idLibrary,
                        principalTable: "Bibliotheque",
                        principalColumn: "IdLibrary");
                    table.ForeignKey(
                        name: "FK_Transactions_Histoire_IdStory",
                        column: x => x.IdStory,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling");
                    table.ForeignKey(
                        name: "FK_Transactions_UserIntern_User_id",
                        column: x => x.User_id,
                        principalTable: "UserIntern",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "Library_BoxStories",
                columns: table => new
                {
                    IdLibrary = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdStoryTellBox = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Bibliotheque_users Property",
                table: "Bibliotheque",
                column: "users Property",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Box de Bibliotheque_IdStoryTell",
                table: "Box de Bibliotheque",
                column: "IdStoryTell");

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_Idimage",
                table: "Chapitre",
                column: "Idimage");

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_Idstory",
                table: "Chapitre",
                column: "Idstory");

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_IdstoryTelling",
                table: "Chapitre",
                column: "IdstoryTelling");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_IdStoryTell",
                table: "Commentaire",
                column: "IdStoryTell");

            migrationBuilder.CreateIndex(
                name: "IX_Forfait_RoleId",
                table: "Forfait",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Histoire_Owner",
                table: "Histoire",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Idees_IdStoryTell",
                table: "Idees",
                column: "IdStoryTell");

            migrationBuilder.CreateIndex(
                name: "IX_Image_IdTag",
                table: "Image",
                column: "IdTag");

            migrationBuilder.CreateIndex(
                name: "IX_Library_BoxStories_IdLibrary",
                table: "Library_BoxStories",
                column: "IdLibrary");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idLibrary",
                table: "Transactions",
                column: "idLibrary");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IdStory",
                table: "Transactions",
                column: "IdStory");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_User_id",
                table: "Transactions",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role",
                table: "User",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_UserIntern_Forfait reference",
                table: "UserIntern",
                column: "Forfait reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserIntern_User Identity",
                table: "UserIntern",
                column: "User Identity",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapitre");

            migrationBuilder.DropTable(
                name: "Commentaire");

            migrationBuilder.DropTable(
                name: "Idees");

            migrationBuilder.DropTable(
                name: "Library_BoxStories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "Box de Bibliotheque");

            migrationBuilder.DropTable(
                name: "Bibliotheque");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Histoire");

            migrationBuilder.DropTable(
                name: "UserIntern");

            migrationBuilder.DropTable(
                name: "Forfait");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
