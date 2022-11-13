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
    internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(item => item.IdRole);
            builder.Property(e => e.RoleLibelle).HasColumnName("Role").IsRequired();
            builder.Property(e => e.IsAdmin).HasColumnName("Admin").HasDefaultValue(false);
            builder.Property(e => e.AuthRoleId).HasColumnName("Auth0IdReference").IsRequired();
        }
    }
}
