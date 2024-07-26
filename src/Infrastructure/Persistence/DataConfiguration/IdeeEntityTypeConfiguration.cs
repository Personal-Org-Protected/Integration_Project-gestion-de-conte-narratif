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
    public class IdeeEntityTypeConfiguration : IEntityTypeConfiguration<Idees>
    {
        public void Configure(EntityTypeBuilder<Idees> builder)
        {
            builder.ToTable("Idees");
            builder.HasKey(item=>item.IdIdee);
            builder.Property(e => e.Idea).HasColumnName("Idea").IsRequired();
            builder.Property(e=>e.IdStoryTelling).HasColumnName("IdStoryTell").IsRequired();
            builder.HasOne(f=>f.StoryTelling)
                .WithMany(d=>d.Idees)
                .HasForeignKey(f=>f.IdStoryTelling)
                .HasConstraintName("History_FK");
        }
    }
}
