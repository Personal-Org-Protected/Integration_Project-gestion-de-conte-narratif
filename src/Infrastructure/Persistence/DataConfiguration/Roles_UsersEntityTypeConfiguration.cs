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
    public class Roles_UsersEntityTypeConfiguration : IEntityTypeConfiguration<Roles_Users>
    {
        public void Configure(EntityTypeBuilder<Roles_Users> builder)
        {
            builder.ToTable("Users_Roles");
            builder.HasKey(e => new { e.user_id, e.idRole });
            builder.HasOne(e => e.Roles)
            .WithMany(p => p.UsersRoles);
            builder.HasOne(e => e.User)
            .WithMany(p => p.UsersRoles);
        }
    }
}
