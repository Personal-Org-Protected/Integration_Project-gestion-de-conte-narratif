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
    //new
    public class CommentairesEntityTypeConfiguration : IEntityTypeConfiguration<Commentaires>
    {
        public void Configure(EntityTypeBuilder<Commentaires> builder)
        {
            builder.ToTable("Commentaire");
            builder.HasKey(item=>item.IdCommentaire);
            builder.Property(e => e.Commentaire).HasColumnName("commentaire").IsRequired();
            builder.Property(e => e.Owner).HasColumnName("Owner").IsRequired();
            builder.Property(e => e.IdZone).HasColumnName("Zone de Commenataire").IsRequired();
            builder.Property(e => e.signal).HasColumnName("Signalement");
            builder.Property(e => e.DateCreation).HasColumnName("Date de creation");
            builder.HasOne(f => f.ZoneCommentary)
                .WithMany(d => d.Commentaires)
                .HasForeignKey(f => f.IdZone)
                .HasConstraintName("Zone Comm");
        }
    }
}
