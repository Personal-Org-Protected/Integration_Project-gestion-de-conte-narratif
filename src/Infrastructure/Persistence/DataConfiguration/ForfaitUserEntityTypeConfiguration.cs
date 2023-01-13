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
    public class ForfaitUserEntityTypeConfiguration : IEntityTypeConfiguration<Forfait_UserIntern>
    {
        public void Configure(EntityTypeBuilder<Forfait_UserIntern> builder)
        {
            builder.ToTable("Forfait_User");
            builder.HasKey(e => new { e.user_id, e.IdForfait });
            builder.HasOne(e => e.User)
            .WithMany(p => p.ForfaitUser);
            builder.HasOne(e => e.ForfaitClient)
            .WithMany(p => p.ForfaitUser);
        }
    }
}
