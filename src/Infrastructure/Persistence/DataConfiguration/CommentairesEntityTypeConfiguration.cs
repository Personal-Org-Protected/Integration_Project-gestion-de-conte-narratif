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
            builder.Property(e => e.user_id).HasColumnName("Proprietaire").IsRequired();
            builder.Property(e => e.IdZone).HasColumnName("Zone de Commenataire").IsRequired();
            builder.Property(e => e.signal).HasColumnName("Signalement").HasDefaultValue(0);
            builder.Property(e => e.like).HasColumnName("Like").HasDefaultValue(0);
            builder.Property(e => e.DateCreation).HasColumnType("SMALLDATETIME").HasColumnName("Date de creation");
            builder.HasOne(f => f.ZoneCommentary)
                .WithMany(d => d.Commentaires)
                .HasForeignKey(f => f.IdZone)
                .HasConstraintName("Zone Comm");
        }
    }
}
