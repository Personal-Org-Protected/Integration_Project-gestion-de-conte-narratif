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
    public class BasketItemEntityTypeConfiguration : IEntityTypeConfiguration<BasketItems>
    {
        public void Configure(EntityTypeBuilder<BasketItems> builder)
        {
            builder.ToTable("BasketItems");
            builder.HasKey(e => new { e.IdStoryTelling, e.basket_id });
            builder.HasOne(e => e.Basket)
            .WithMany(p => p.Items);
            builder.HasOne(e => e.StoryTelling)
            .WithMany(p => p.Items);
        }
    }
}
