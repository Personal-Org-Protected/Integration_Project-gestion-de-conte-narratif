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

        private bool disposed = false;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior =
            QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public ApplicationDbContext()
        {
            ChangeTracker.QueryTrackingBehavior =
            QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=WP4163;Database=StoryTelling;User Id=StoryTell_user;Password=Mouhsine1998*;Integrated Security=true;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        /// <summary>
        /// cette partier n'est plsu necessaire le boulot est passé à l'unit of work
        /// </summary>
        public DbSet<Image> Images { get; set; }

        public DbSet<Story> Stories { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<StoryTelling> StoryTellings { get; set; }
        public DbSet<Chapitre> Chapitres { get; set;}
        public DbSet<Idees> Ideas { get; set; }
        public DbSet<Commentaires> Commentaries { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<StoryTellBox> storyTellBoxes { get; set; }
        //public DbSet<Library_stories> library_Stories { get; set; }
        public DbSet<ZoneCommentary> ZoneComments { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItems> Basket_items { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RatingInfos> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            //Todo add code for auditableEntity
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task beginTransactionAsync()
        {
           await base.Database.BeginTransactionAsync();
        }
        public async Task commitTransactionAsync()
        {
            await base.Database.CommitTransactionAsync();
        }
        public async Task rollbackTransactionAsync()
        {
            await base.Database.RollbackTransactionAsync();
        }

        protected async virtual Task Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await base.DisposeAsync();
                }
            }
            this.disposed = true;
        }


        public async Task Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
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
            builder.ApplyConfiguration(new IdeeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CommentairesEntityTypeConfiguration());
            builder.ApplyConfiguration(new LibraryEntityTypeConfiguration());
            builder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
            builder.ApplyConfiguration(new StoryTellBoxEntityTypeConfiguration());
            builder.ApplyConfiguration(new ZoneCommentaryEntityTypeConfiguration());
            builder.ApplyConfiguration(new BasketEntityTypeConfiguration());
            builder.ApplyConfiguration(new BasketItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new NotificationsEntityTypeConfiguration());
            builder.ApplyConfiguration(new RatingEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserIdentityTypeConfiguration());


        }

    }
}
