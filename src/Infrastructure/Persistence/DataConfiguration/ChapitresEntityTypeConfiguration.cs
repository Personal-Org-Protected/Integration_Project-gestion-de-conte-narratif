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
    public class ChapitresEntityTypeConfiguration : IEntityTypeConfiguration<Chapitre>
    {
        public void Configure(EntityTypeBuilder<Chapitre> builder)
        {
            builder.ToTable("Chapitre");
            builder.HasKey(item => item.IdChapitre);
            builder.Property(e => e.IdImage).HasColumnName("Idimage").IsRequired();
            builder.Property(e => e.IdStory).HasColumnName("Idstory").IsRequired();
            builder.Property(e => e.IdStoryTelling).HasColumnName("IdstoryTelling").IsRequired();
            builder.Property(e=>e.Order).HasColumnName("order").IsRequired();
            builder.HasOne(f => f.StoryTelling)
                .WithMany(d => d.Chapitres)
                .HasForeignKey(f => f.IdStoryTelling)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Histoire_FK");
            builder.HasOne(f => f.Image)
               .WithMany(d => d.Chapitres)
               .HasForeignKey(f => f.IdImage)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("Image_FK");
        }
    }
}
