using Domain.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserAccountDbContext : IdentityDbContext
    {
        private IConfiguration _configuration;
        private bool disposed = false;

        public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior =
            QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ConnectionIdentity")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

    }
}
