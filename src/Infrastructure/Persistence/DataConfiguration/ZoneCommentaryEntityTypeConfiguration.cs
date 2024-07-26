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
    public class ZoneCommentaryEntityTypeConfiguration : IEntityTypeConfiguration<ZoneCommentary>
    {
        public void Configure(EntityTypeBuilder<ZoneCommentary> builder)
        {
            builder.ToTable("Zone de Commentaire");
            builder.HasKey(item => item.IdZone);
            builder.Property(e=>e.Activated).HasColumnName("Actif").HasDefaultValue(false);
            builder.HasOne(f => f.StoryTelling)
                .WithOne(d => d.ZoneCommentary)
                .HasForeignKey<StoryTelling>(e => e.IdZone)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
