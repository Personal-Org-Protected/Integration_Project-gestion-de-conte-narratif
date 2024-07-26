﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221218111311_NewUpdates1_1")]
    partial class NewUpdates1_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Chapitre", b =>
                {
                    b.Property<int>("IdChapitre")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdChapitre"), 1L, 1);

                    b.Property<int>("IdImage")
                        .HasColumnType("int")
                        .HasColumnName("Idimage");

                    b.Property<int>("IdStory")
                        .HasColumnType("int")
                        .HasColumnName("Idstory");

                    b.Property<int>("IdStoryTelling")
                        .HasColumnType("int")
                        .HasColumnName("IdstoryTelling");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("order");

                    b.HasKey("IdChapitre");

                    b.HasIndex("IdImage");

                    b.HasIndex("IdStory")
                        .IsUnique();

                    b.HasIndex("IdStoryTelling");

                    b.ToTable("Chapitre", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Commentaires", b =>
                {
                    b.Property<int>("IdCommentaire")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCommentaire"), 1L, 1);

                    b.Property<string>("Commentaire")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("commentaire");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date de creation");

                    b.Property<int>("IdZone")
                        .HasColumnType("int")
                        .HasColumnName("Zone de Commenataire");

                    b.Property<int>("signal")
                        .HasColumnType("int")
                        .HasColumnName("Signalement");

                    b.Property<string>("user_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Proprietaire");

                    b.HasKey("IdCommentaire");

                    b.HasIndex("IdZone");

                    b.ToTable("Commentaire", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Forfait_UserIntern", b =>
                {
                    b.Property<string>("user_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdForfait")
                        .HasColumnType("int");

                    b.HasKey("user_id", "IdForfait");

                    b.HasIndex("IdForfait");

                    b.ToTable("Forfait_User", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ForfaitClient", b =>
                {
                    b.Property<int>("IdForfait")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdForfait"), 1L, 1);

                    b.Property<string>("ForfaitLibelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nom de Forfait");

                    b.Property<double>("ForfaitValue")
                        .HasColumnType("float")
                        .HasColumnName("valeur de forfait");

                    b.Property<bool>("IsForAuthor")
                        .HasColumnType("bit")
                        .HasColumnName("Author");

                    b.Property<double>("Reduction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0)
                        .HasColumnName("reduction forfait");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("IdForfait");

                    b.HasIndex("RoleId");

                    b.ToTable("Forfait", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Idees", b =>
                {
                    b.Property<int>("IdIdee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdIdee"), 1L, 1);

                    b.Property<int>("IdStoryTelling")
                        .HasColumnType("int")
                        .HasColumnName("IdStoryTell");

                    b.Property<string>("Idea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Idea");

                    b.HasKey("IdIdee");

                    b.HasIndex("IdStoryTelling");

                    b.ToTable("Idees", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Property<int>("IdImage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdImage"), 1L, 1);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("date")
                        .HasColumnName("Date de Creation");

                    b.Property<DateTime>("DateModif")
                        .HasColumnType("date")
                        .HasColumnName("Date de Modification");

                    b.Property<int>("IdTag")
                        .HasColumnType("int");

                    b.Property<string>("NomImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nom");

                    b.Property<string>("PathImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Path");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("uri");

                    b.Property<string>("descriptionImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("user_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Proprietaire");

                    b.HasKey("IdImage");

                    b.HasIndex("IdTag");

                    b.HasIndex("user_id");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Library", b =>
                {
                    b.Property<string>("IdLibrary")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameLibrary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nom de Bibliotheque");

                    b.Property<string>("user_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Proprietaire");

                    b.HasKey("IdLibrary");

                    b.HasIndex("user_id")
                        .IsUnique();

                    b.ToTable("Bibliotheque", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Story", b =>
                {
                    b.Property<int>("IdStory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStory"), 1L, 1);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("date")
                        .HasColumnName("Date de Creation");

                    b.Property<DateTime>("DateModif")
                        .HasColumnType("date")
                        .HasColumnName("Date de Modification");

                    b.Property<string>("NomStory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Chapitre");

                    b.Property<string>("TextStory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Fond du Chapitre");

                    b.HasKey("IdStory");

                    b.ToTable("Story", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.StoryTellBox", b =>
                {
                    b.Property<int>("IdBox")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBox"), 1L, 1);

                    b.Property<string>("IdLibrary")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdStoryTell")
                        .HasColumnType("int");

                    b.Property<int>("lastPageChecked")
                        .HasColumnType("int")
                        .HasColumnName("Dernier chapitre lu");

                    b.HasKey("IdBox");

                    b.HasIndex("IdLibrary");

                    b.HasIndex("IdStoryTell");

                    b.ToTable("Box d'Histoire", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.StoryTelling", b =>
                {
                    b.Property<int>("IdStoryTelling")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStoryTelling"), 1L, 1);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date de creation de l'Histoire");

                    b.Property<bool>("Finished")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Vendable");

                    b.Property<int>("IdZone")
                        .HasColumnType("int");

                    b.Property<string>("NameStory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nom Histoire");

                    b.Property<string>("Sypnopsis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Resume");

                    b.Property<int?>("idTag")
                        .HasColumnType("int");

                    b.Property<int>("numberRef")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("nombre de vente");

                    b.Property<double>("price")
                        .HasColumnType("float")
                        .HasColumnName("Prix");

                    b.Property<string>("url")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image vignette");

                    b.Property<string>("user_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Proprietaire");

                    b.HasKey("IdStoryTelling");

                    b.HasIndex("IdZone")
                        .IsUnique();

                    b.HasIndex("idTag");

                    b.HasIndex("user_id");

                    b.ToTable("Histoire", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Tag", b =>
                {
                    b.Property<int>("IdTag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTag"), 1L, 1);

                    b.Property<string>("NameTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Libelle");

                    b.Property<double>("numberRef")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0)
                        .HasColumnName("Nombre de reference");

                    b.HasKey("IdTag");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"), 1L, 1);

                    b.Property<DateTime>("DateTransaction")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date de la transaction");

                    b.Property<string>("NameBook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nom du livre");

                    b.Property<int>("StoryTellId")
                        .HasColumnType("int")
                        .HasColumnName("IdStory");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("user_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TransactionId");

                    b.HasIndex("StoryTellId");

                    b.HasIndex("user_id");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ZoneCommentary", b =>
                {
                    b.Property<int>("IdZone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdZone"), 1L, 1);

                    b.Property<bool>("Activated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Actif");

                    b.HasKey("IdZone");

                    b.ToTable("Zone de Commentaire", (string)null);
                });

            modelBuilder.Entity("Domain.Identity.Roles", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRole"), 1L, 1);

                    b.Property<string>("AuthRoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Auth0IdReference");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Admin");

                    b.Property<string>("RoleLibelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Role");

                    b.HasKey("IdRole");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Domain.Identity.Roles_Users", b =>
                {
                    b.Property<string>("user_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("idRole")
                        .HasColumnType("int");

                    b.HasKey("user_id", "idRole");

                    b.HasIndex("idRole");

                    b.ToTable("Users_Roles", (string)null);
                });

            modelBuilder.Entity("Domain.Identity.User", b =>
                {
                    b.Property<string>("user_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("Birth Date");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Region");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Password");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("UserName");

                    b.HasKey("user_id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Chapitre", b =>
                {
                    b.HasOne("Domain.Entities.Image", "Image")
                        .WithMany("Chapitres")
                        .HasForeignKey("IdImage")
                        .IsRequired()
                        .HasConstraintName("Image_FK");

                    b.HasOne("Domain.Entities.Story", "Story")
                        .WithOne("Chapitre")
                        .HasForeignKey("Domain.Entities.Chapitre", "IdStory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Story_FK");

                    b.HasOne("Domain.Entities.StoryTelling", "StoryTelling")
                        .WithMany("Chapitres")
                        .HasForeignKey("IdStoryTelling")
                        .IsRequired()
                        .HasConstraintName("Histoire_FK");

                    b.Navigation("Image");

                    b.Navigation("Story");

                    b.Navigation("StoryTelling");
                });

            modelBuilder.Entity("Domain.Entities.Commentaires", b =>
                {
                    b.HasOne("Domain.Entities.ZoneCommentary", "ZoneCommentary")
                        .WithMany("Commentaires")
                        .HasForeignKey("IdZone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Zone Comm");

                    b.Navigation("ZoneCommentary");
                });

            modelBuilder.Entity("Domain.Entities.Forfait_UserIntern", b =>
                {
                    b.HasOne("Domain.Entities.ForfaitClient", "ForfaitClient")
                        .WithMany("ForfaitUser")
                        .HasForeignKey("IdForfait")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Identity.User", "User")
                        .WithMany("ForfaitUser")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForfaitClient");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.ForfaitClient", b =>
                {
                    b.HasOne("Domain.Identity.Roles", "Roles")
                        .WithMany("ForfaitClients")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Idees", b =>
                {
                    b.HasOne("Domain.Entities.StoryTelling", "StoryTelling")
                        .WithMany("Idees")
                        .HasForeignKey("IdStoryTelling")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("History_FK");

                    b.Navigation("StoryTelling");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.HasOne("Domain.Entities.Tag", "Tags")
                        .WithMany("Image")
                        .HasForeignKey("IdTag")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Tag_FK");

                    b.HasOne("Domain.Identity.User", "User")
                        .WithMany("Images")
                        .HasForeignKey("user_id")
                        .IsRequired();

                    b.Navigation("Tags");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Library", b =>
                {
                    b.HasOne("Domain.Identity.User", "Owner")
                        .WithOne("Library")
                        .HasForeignKey("Domain.Entities.Library", "user_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.StoryTellBox", b =>
                {
                    b.HasOne("Domain.Entities.Library", "StoryTellLibrary")
                        .WithMany("StoryTellBoxes")
                        .HasForeignKey("IdLibrary")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Library_ref");

                    b.HasOne("Domain.Entities.StoryTelling", "StoryTelling")
                        .WithMany("StoryTellBox")
                        .HasForeignKey("IdStoryTell")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("StoryTellLibrary");

                    b.Navigation("StoryTelling");
                });

            modelBuilder.Entity("Domain.Entities.StoryTelling", b =>
                {
                    b.HasOne("Domain.Entities.ZoneCommentary", "ZoneCommentary")
                        .WithOne("StoryTelling")
                        .HasForeignKey("Domain.Entities.StoryTelling", "IdZone")
                        .IsRequired();

                    b.HasOne("Domain.Entities.Tag", "Tags")
                        .WithMany("StoryTellings")
                        .HasForeignKey("idTag")
                        .OnDelete(DeleteBehavior.ClientNoAction);

                    b.HasOne("Domain.Identity.User", "User")
                        .WithMany("Stories")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tags");

                    b.Navigation("User");

                    b.Navigation("ZoneCommentary");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Domain.Entities.StoryTelling", "StoryTelling")
                        .WithMany("Transactions")
                        .HasForeignKey("StoryTellId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Identity.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("StoryTelling");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Identity.Roles_Users", b =>
                {
                    b.HasOne("Domain.Identity.Roles", "Roles")
                        .WithMany("UsersRoles")
                        .HasForeignKey("idRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Identity.User", "User")
                        .WithMany("UsersRoles")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.ForfaitClient", b =>
                {
                    b.Navigation("ForfaitUser");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Navigation("Chapitres");
                });

            modelBuilder.Entity("Domain.Entities.Library", b =>
                {
                    b.Navigation("StoryTellBoxes");
                });

            modelBuilder.Entity("Domain.Entities.Story", b =>
                {
                    b.Navigation("Chapitre")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.StoryTelling", b =>
                {
                    b.Navigation("Chapitres");

                    b.Navigation("Idees");

                    b.Navigation("StoryTellBox");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.Tag", b =>
                {
                    b.Navigation("Image");

                    b.Navigation("StoryTellings");
                });

            modelBuilder.Entity("Domain.Entities.ZoneCommentary", b =>
                {
                    b.Navigation("Commentaires");

                    b.Navigation("StoryTelling")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Identity.Roles", b =>
                {
                    b.Navigation("ForfaitClients");

                    b.Navigation("UsersRoles");
                });

            modelBuilder.Entity("Domain.Identity.User", b =>
                {
                    b.Navigation("ForfaitUser");

                    b.Navigation("Images");

                    b.Navigation("Library")
                        .IsRequired();

                    b.Navigation("Stories");

                    b.Navigation("Transactions");

                    b.Navigation("UsersRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
