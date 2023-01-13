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
    public class LibraryEntityTypeConfiguration : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.ToTable("Bibliotheque");
            builder.HasKey(item => item.IdLibrary);
            builder.Property(e => e.NameLibrary).HasColumnName("Nom de Bibliotheque");
            builder.Property(e => e.user_id).HasColumnName("Proprietaire");//new
        }
    }
}
