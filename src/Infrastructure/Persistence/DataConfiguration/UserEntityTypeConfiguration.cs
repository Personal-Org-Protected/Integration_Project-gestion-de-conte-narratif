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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("UserIntern");
            builder.HasKey(e=>e.IdUser);
            builder.Property(e => e.user_id).HasColumnName("User Identity").IsRequired();
            builder.HasOne(f => f.Library)
                .WithOne(d => d.Owner)
                .HasForeignKey<Library>(e => e.IdUser)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
