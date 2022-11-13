using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DataConfiguration
{
    public class UserIdentityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(item => item.user_id);
            builder.Property(e => e.username).HasColumnName("UserName").IsRequired().HasMaxLength(15);
            builder.Property(e => e.Location).HasColumnName("Region").IsRequired();
            builder.Property(e => e.email).HasColumnName("Email").IsRequired();
            builder.Property(e => e.BirthDate).HasColumnName("Birth Date").IsRequired();
            builder.Property(e => e.password).HasColumnName("Password").IsRequired().HasMaxLength(20);
            builder.HasOne(f => f.UserEntity)
                .WithOne(d => d.user)
                .HasForeignKey<UserEntity>(f => f.user_id);//new
        }
    }
}
