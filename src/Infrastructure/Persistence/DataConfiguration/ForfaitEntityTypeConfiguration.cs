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
    public class ForfaitEntityTypeConfiguration : IEntityTypeConfiguration<ForfaitClient>
    {
        public void Configure(EntityTypeBuilder<ForfaitClient> builder)
        {
            builder.ToTable("Forfait");
            builder.HasKey(item => item.IdForfait);
            builder.Property(e => e.ForfaitLibelle).HasColumnName("nom de Forfait");
            builder.Property(e => e.ForfaitValue).HasColumnName("valeur de forfait");
            builder.Property(e=>e.IsForAuthor).HasColumnName("Author");
            builder.Property(e => e.Reduction).HasColumnName("reduction forfait").HasDefaultValue(0);
            builder.HasOne(f=>f.Roles)
                .WithMany(d=>d.ForfaitClients)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
