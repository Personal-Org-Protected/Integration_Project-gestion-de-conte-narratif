using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitializeDatabase : Migration
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
                    Admin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Auth0IdReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombredereference = table.Column<double>(name: "Nombre de reference", type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.IdTag);
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });

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

            migrationBuilder.CreateTable(
                name: "Forfait",
                columns: table => new
                {
                    IdForfait = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomdeForfait = table.Column<string>(name: "nom de Forfait", type: "nvarchar(max)", nullable: false),
                    valeurdeforfait = table.Column<double>(name: "valeur de forfait", type: "float", nullable: false),
                    Author = table.Column<bool>(type: "bit", nullable: false),
                    reductionforfait = table.Column<double>(name: "reduction forfait", type: "float", nullable: false, defaultValue: 0.0),
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
                name: "Bibliotheque",
                columns: table => new
                {
                    IdLibrary = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomdeBibliotheque = table.Column<string>(name: "Nom de Bibliotheque", type: "nvarchar(max)", nullable: false),
                    Proprietaire = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibliotheque", x => x.IdLibrary);
                    table.ForeignKey(
                        name: "FK_Bibliotheque_User_Proprietaire",
                        column: x => x.Proprietaire,
                        principalTable: "User",
                        principalColumn: "user_id");
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
                    Proprietaire = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdTag = table.Column<int>(type: "int", nullable: false),
                    DatedeCreation = table.Column<DateTime>(name: "Date de Creation", type: "date", nullable: false),
                    DatedeModification = table.Column<DateTime>(name: "Date de Modification", type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.IdImage);
                    table.ForeignKey(
                        name: "FK_Image_User_Proprietaire",
                        column: x => x.Proprietaire,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "Tag_FK",
                        column: x => x.IdTag,
                        principalTable: "Tag",
                        principalColumn: "IdTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_Roles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Roles", x => new { x.user_id, x.idRole });
                    table.ForeignKey(
                        name: "FK_Users_Roles_Roles_idRole",
                        column: x => x.idRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentaire",
                columns: table => new
                {
                    IdCommentaire = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proprietaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signalement = table.Column<int>(type: "int", nullable: false),
                    commentaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZonedeCommenataire = table.Column<int>(name: "Zone de Commenataire", type: "int", nullable: false),
                    Datedecreation = table.Column<DateTime>(name: "Date de creation", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaire", x => x.IdCommentaire);
                    table.ForeignKey(
                        name: "Zone Comm",
                        column: x => x.ZonedeCommenataire,
                        principalTable: "Zone de Commentaire",
                        principalColumn: "IdZone",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histoire",
                columns: table => new
                {
                    IdStoryTelling = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagevignette = table.Column<string>(name: "image vignette", type: "nvarchar(max)", nullable: true),
                    NomHistoire = table.Column<string>(name: "Nom Histoire", type: "nvarchar(max)", nullable: false),
                    Proprietaire = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    nombredevente = table.Column<int>(name: "nombre de vente", type: "int", nullable: false, defaultValue: 0),
                    idTag = table.Column<int>(type: "int", nullable: true),
                    DatedecreationdelHistoire = table.Column<DateTime>(name: "Date de creation de l'Histoire", type: "datetime2", nullable: false),
                    IdZone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histoire", x => x.IdStoryTelling);
                    table.ForeignKey(
                        name: "FK_Histoire_Tag_idTag",
                        column: x => x.idTag,
                        principalTable: "Tag",
                        principalColumn: "IdTag");
                    table.ForeignKey(
                        name: "FK_Histoire_User_Proprietaire",
                        column: x => x.Proprietaire,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histoire_Zone de Commentaire_IdZone",
                        column: x => x.IdZone,
                        principalTable: "Zone de Commentaire",
                        principalColumn: "IdZone");
                });

            migrationBuilder.CreateTable(
                name: "Forfait_User",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdForfait = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forfait_User", x => new { x.user_id, x.IdForfait });
                    table.ForeignKey(
                        name: "FK_Forfait_User_Forfait_IdForfait",
                        column: x => x.IdForfait,
                        principalTable: "Forfait",
                        principalColumn: "IdForfait",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forfait_User_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Box d'Histoire",
                columns: table => new
                {
                    IdBox = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dernierchapitrelu = table.Column<int>(name: "Dernier chapitre lu", type: "int", nullable: false),
                    IdStoryTell = table.Column<int>(type: "int", nullable: false),
                    IdLibrary = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box d'Histoire", x => x.IdBox);
                    table.ForeignKey(
                        name: "FK_Box d'Histoire_Histoire_IdStoryTell",
                        column: x => x.IdStoryTell,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling");
                    table.ForeignKey(
                        name: "Library_ref",
                        column: x => x.IdLibrary,
                        principalTable: "Bibliotheque",
                        principalColumn: "IdLibrary");
                });

            migrationBuilder.CreateTable(
                name: "Chapitre",
                columns: table => new
                {
                    IdChapitre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idimage = table.Column<int>(type: "int", nullable: false),
                    Idstory = table.Column<int>(type: "int", nullable: false),
                    IdstoryTelling = table.Column<int>(type: "int", nullable: false),
                    order = table.Column<int>(type: "int", nullable: false)
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
                        principalColumn: "IdStory",
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
                    IdStory = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Histoire_IdStory",
                        column: x => x.IdStory,
                        principalTable: "Histoire",
                        principalColumn: "IdStoryTelling");
                    table.ForeignKey(
                        name: "FK_Transactions_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bibliotheque_Proprietaire",
                table: "Bibliotheque",
                column: "Proprietaire",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Box d'Histoire_IdLibrary",
                table: "Box d'Histoire",
                column: "IdLibrary");

            migrationBuilder.CreateIndex(
                name: "IX_Box d'Histoire_IdStoryTell",
                table: "Box d'Histoire",
                column: "IdStoryTell");

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_Idimage",
                table: "Chapitre",
                column: "Idimage");

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_Idstory",
                table: "Chapitre",
                column: "Idstory",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapitre_IdstoryTelling",
                table: "Chapitre",
                column: "IdstoryTelling");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_Zone de Commenataire",
                table: "Commentaire",
                column: "Zone de Commenataire");

            migrationBuilder.CreateIndex(
                name: "IX_Forfait_RoleId",
                table: "Forfait",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Forfait_User_IdForfait",
                table: "Forfait_User",
                column: "IdForfait");

            migrationBuilder.CreateIndex(
                name: "IX_Histoire_idTag",
                table: "Histoire",
                column: "idTag");

            migrationBuilder.CreateIndex(
                name: "IX_Histoire_IdZone",
                table: "Histoire",
                column: "IdZone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histoire_Proprietaire",
                table: "Histoire",
                column: "Proprietaire");

            migrationBuilder.CreateIndex(
                name: "IX_Idees_IdStoryTell",
                table: "Idees",
                column: "IdStoryTell");

            migrationBuilder.CreateIndex(
                name: "IX_Image_IdTag",
                table: "Image",
                column: "IdTag");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Proprietaire",
                table: "Image",
                column: "Proprietaire");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IdStory",
                table: "Transactions",
                column: "IdStory");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_user_id",
                table: "Transactions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roles_idRole",
                table: "Users_Roles",
                column: "idRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Box d'Histoire");

            migrationBuilder.DropTable(
                name: "Chapitre");

            migrationBuilder.DropTable(
                name: "Commentaire");

            migrationBuilder.DropTable(
                name: "Forfait_User");

            migrationBuilder.DropTable(
                name: "Idees");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users_Roles");

            migrationBuilder.DropTable(
                name: "Bibliotheque");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "Forfait");

            migrationBuilder.DropTable(
                name: "Histoire");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Zone de Commentaire");
        }
    }
}
