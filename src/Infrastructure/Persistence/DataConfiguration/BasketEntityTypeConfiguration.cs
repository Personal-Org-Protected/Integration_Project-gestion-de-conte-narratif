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
    public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Panier");
            builder.HasKey(item => item.basket_id);
            builder.Property(e => e.user_id).HasColumnName("Proprietaire").IsRequired();
            builder.Property(e => e.isEmpty).HasColumnName("Panier vide").HasDefaultValue(false).IsRequired();
        }
    }
}
