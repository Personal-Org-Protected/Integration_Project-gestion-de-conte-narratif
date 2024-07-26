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
    public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<RatingInfos>
    {
        public void Configure(EntityTypeBuilder<RatingInfos> builder)
        {
            builder.ToTable("Notes");
            builder.HasKey(e => new { e.user_id, e.storyTellId });
            builder.Property(e => e.note).HasColumnName("note").HasDefaultValue(0).IsRequired();
            builder.HasOne(e => e.User)
            .WithMany(p => p.Ratings).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.User)
            .WithMany(p => p.Ratings).OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
