using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DataConfiguration
{
    public class NotificationsEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification");
            builder.HasKey(item => item.idNotification);
            builder.Property(e => e.title).HasColumnName("title").IsRequired();
            builder.Property(e => e.message).HasColumnName("message").IsRequired();
            builder.Property(e => e.read).HasColumnName("Lu").IsRequired().HasDefaultValue(false);
            builder.Property(e => e.created).HasColumnType("date").HasColumnName("date de notif").IsRequired();
            builder.Property(e => e.user_id).HasColumnName("target").IsRequired();
            builder.HasOne(f => f.User)
                .WithMany(d => d.Notification)
                .HasForeignKey(e => e.user_id);
        }
    }
}
