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
    public class StoryTellingEntityTypeConfiguration : IEntityTypeConfiguration<StoryTelling>
    {
        public void Configure(EntityTypeBuilder<StoryTelling> builder)
        {
            builder.ToTable("Histoire");
            builder.HasKey(item => item.IdStoryTelling);
            builder.Property(e => e.NameStory).HasColumnName("Nom Histoire");
            builder.Property(e => e.url).HasColumnName("image vignette").IsRequired(false);
            builder.Property(e => e.user_id).HasColumnName("Proprietaire");//new
            builder.Property(e => e.price).HasColumnName("Prix");//new
            builder.Property(e => e.Sypnopsis).HasColumnName("Resume");//new
            builder.Property(e => e.DateCreation).HasColumnType("date").HasColumnName("Date de creation de l'Histoire");//new
            builder.Property(e => e.Finished).HasColumnName("Vendable").HasDefaultValue(false);
            builder.Property(e => e.numberRef).HasColumnName("nombre de vente").HasDefaultValue(0);
            builder.HasOne(f => f.User)
                .WithMany(d => d.Stories)
                .HasForeignKey(e => e.user_id);
            builder.HasOne(f=>f.Tags)
                .WithMany(d=>d.StoryTellings)
                .HasForeignKey(e=>e.idTag)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .IsRequired(false);

        }
    }
}
