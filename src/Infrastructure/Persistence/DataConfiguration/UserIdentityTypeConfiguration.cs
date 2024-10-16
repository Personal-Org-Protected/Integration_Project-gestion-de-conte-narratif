﻿using Domain.Entities;
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
            builder.Property(e => e.BirthDate).HasColumnName("Birth Date").HasColumnType("date").IsRequired();
            builder.Property(e => e.password).HasColumnName("Password").IsRequired().HasMaxLength(20);
            builder.Property(e => e.description).HasColumnName("Avatar").IsRequired(false);
            builder.Property(e => e.description).HasColumnName("Description").IsRequired(false);
            builder.HasOne(f => f.Library)
                 .WithOne(d => d.Owner)
                 .HasForeignKey<Library>(e => e.user_id)
                 .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(f => f.Basket)
               .WithOne(d => d.User)
               .HasForeignKey<Basket>(e => e.user_id)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
