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
    public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Image");
            builder.HasKey(item => item.IdImage);
            builder.Property(e => e.NomImage).HasColumnName("Nom").IsRequired();
            builder.Property(e => e.PathImage).HasColumnName("Path").IsRequired();
            builder.Property(e => e.Uri).HasColumnName("uri");
            builder.Property(e => e.descriptionImage).HasColumnName("Description");
            builder.Property(e => e.DateCreation).HasColumnName("Date de Creation").HasColumnType("date");
            builder.Property(e => e.DateModif).HasColumnName("Date de Modification").HasColumnType("date");
            builder.HasOne(e => e.Tags).WithMany(f => f.Image).HasForeignKey(e => e.IdTag).HasConstraintName("Tag_FK");
            builder.HasOne(f => f.User)
                .WithMany(d => d.Images)
                .HasForeignKey(e => e.owner)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
