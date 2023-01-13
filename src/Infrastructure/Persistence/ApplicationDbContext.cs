using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Identity;
using Infrastructure.Persistence.DataConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }

        public DbSet<Story> Stories { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<StoryTelling> StoryTellings { get; set; }
        public DbSet<Chapitre> Chapitres { get; set;}
        public DbSet<ForfaitClient> Forfaits { get; set; }
        public DbSet<Idees> Ideas { get; set; }
        public DbSet<Commentaires> Commentaries { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Forfait_UserIntern> Forfait_Users { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<StoryTellBox> storyTellBoxes { get; set; }
        //public DbSet<Library_stories> library_Stories { get; set; }
        public DbSet<Roles_Users> Roles_Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<ZoneCommentary> ZoneComments { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //Todo add code for auditableEntity
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task beginTransaction()
        {
           await base.Database.BeginTransactionAsync();
        }
        public async Task commitTransaction()
        {
            await base.Database.CommitTransactionAsync();
        }
        public async Task rollbackTransaction()
        {
            await base.Database.RollbackTransactionAsync();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Define some FK for example
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ImageEntityTypeConfiguration());
            builder.ApplyConfiguration(new StoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new TagEntityTypeConfiguration());
            builder.ApplyConfiguration(new StoryTellingEntityTypeConfiguration());
            builder.ApplyConfiguration(new ChapitresEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserIdentityTypeConfiguration());
            builder.ApplyConfiguration(new IdeeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CommentairesEntityTypeConfiguration());
            builder.ApplyConfiguration(new ForfaitEntityTypeConfiguration());
            builder.ApplyConfiguration(new LibraryEntityTypeConfiguration());
            builder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            builder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
            builder.ApplyConfiguration(new StoryTellBoxEntityTypeConfiguration());
            builder.ApplyConfiguration(new Roles_UsersEntityTypeConfiguration());
            builder.ApplyConfiguration(new ForfaitUserEntityTypeConfiguration());
            builder.ApplyConfiguration(new ZoneCommentaryEntityTypeConfiguration());

        }
    }
}
