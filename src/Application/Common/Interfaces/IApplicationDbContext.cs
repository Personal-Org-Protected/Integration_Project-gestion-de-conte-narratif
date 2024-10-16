﻿using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext:IDisposable
    {
        DbSet<Image> Images { get; set; }

        DbSet<Story> Stories { get; set; }

        DbSet<StoryTelling> StoryTellings { get; set; }

        DbSet<Chapitre> Chapitres { get; set; }

        DbSet<Tag> Tag { get; set; }
        DbSet<Idees> Ideas { get; set; }
        DbSet<ForfaitClient> Forfaits { get; set; }
        DbSet<Commentaires> Commentaries { get; set; }
        DbSet<Library> Libraries { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<StoryTellBox> storyTellBoxes { get; set; }

        DbSet<Roles> Roles { get; set; }

        DbSet<Roles_Users> Roles_Users { get; set; }
        DbSet<Forfait_UserIntern> Forfait_Users { get; set; }
        DbSet<ZoneCommentary> ZoneComments { get; set; }

         DbSet<Basket> Basket { get; set; }
         DbSet<BasketItems> Basket_items { get; set; }
         DbSet<Notification> Notifications { get; set; }
        DbSet<RatingInfos> Ratings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task commitTransactionAsync();
        Task beginTransactionAsync();
        Task rollbackTransactionAsync();
        Task Dispose();


    }
}
