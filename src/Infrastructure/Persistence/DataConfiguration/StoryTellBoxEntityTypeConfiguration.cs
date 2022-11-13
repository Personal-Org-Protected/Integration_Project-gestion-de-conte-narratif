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
    public class StoryTellBoxEntityTypeConfiguration : IEntityTypeConfiguration<StoryTellBox>
    {
        public void Configure(EntityTypeBuilder<StoryTellBox> builder)
        {
            builder.ToTable("Box d'Histoire");
            builder.HasKey(item => item.IdBox);
            builder.Property(e => e.lastPageChecked).HasColumnName("Dernier chapitre lu");
            builder.HasOne(f => f.StoryTelling)
                .WithMany(d => d.StoryTellBox)
                .HasForeignKey(e => e.IdStoryTell)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(f => f.StoryTellLibrary)
                .WithMany(d => d.StoryTellBoxes)
                .HasForeignKey(e => e.IdLibrary)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("Library_ref");
        }
    }
}
