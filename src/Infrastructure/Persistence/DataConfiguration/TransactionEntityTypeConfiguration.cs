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
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(item => item.TransactionId);
            builder.Property(e => e.NameBook).HasColumnName("Nom du livre");
            builder.Property(e => e.DateTransaction).HasColumnName("Date de la transaction");
            builder.Property(e => e.StoryTellId).HasColumnName("IdStory");
            builder.HasOne(f=>f.StoryTelling)
                .WithMany(d=>d.Transactions)
                .HasForeignKey(e=>e.StoryTellId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(f => f.User)
                .WithMany(d => d.Transactions)
                .HasForeignKey(e => e.user_id)
                .OnDelete(DeleteBehavior.NoAction);
            //builder.HasOne(f => f.Library)
            //    .WithMany(d => d.TransactionLibrary)
            //    .HasForeignKey(e => e.idLibrary)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
