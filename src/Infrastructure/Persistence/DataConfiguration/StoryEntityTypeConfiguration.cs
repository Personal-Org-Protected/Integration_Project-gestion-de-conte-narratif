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
    public class StoryEntityTypeConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable("Story");
            builder.HasKey(item => item.IdStory);
            builder.Property(e => e.NomStory).HasColumnName("Chapitre").IsRequired();
            builder.Property(e => e.TextStory).HasColumnName("Fond du Chapitre").IsRequired();
            builder.Property(e => e.DateCreation).HasColumnName("Date de Creation").HasColumnType("date");
            builder.Property(e => e.DateModif).HasColumnName("Date de Modification").HasColumnType("date");
            builder.HasOne(f=>f.Chapitre)
                .WithOne(d=>d.Story)
                .HasForeignKey<Chapitre>(e=>e.IdStory)
                .HasConstraintName("Story_FK");
        }
    }
}
