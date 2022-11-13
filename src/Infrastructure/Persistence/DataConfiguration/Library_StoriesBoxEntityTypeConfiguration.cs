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
    public class Library_StoriesBoxEntityTypeConfiguration : IEntityTypeConfiguration<Library_stories>
    {
        public void Configure(EntityTypeBuilder<Library_stories> builder)
        {
         
        }
    }
}
