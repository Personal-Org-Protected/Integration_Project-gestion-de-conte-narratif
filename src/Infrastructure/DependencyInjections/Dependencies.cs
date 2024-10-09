using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjections
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var AppSqlString = new SqlConnectionStringBuilder(configuration.GetConnectionString("ConnectionSecurity"))
            {
                //Password = Environment.GetEnvironmentVariable("StoryTell_DB_Password")
            }.ConnectionString;
            //var AccountSqlString = new SqlConnectionStringBuilder(configuration.GetConnectionString("ConnectionIdentity"))
            //{
            //    //Password = Environment.GetEnvironmentVariable("StoryTell_DB_Password")
            //}.ConnectionString;
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                        AppSqlString));
            //services.AddDbContext<UserAccountDbContext>(options =>
            //options.UseSqlServer(
            //            AccountSqlString));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());//avant c'etait scoped
            services.AddHttpContextAccessor();
            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.User.RequireUniqueEmail = true;
            //    options.SignIn.RequireConfirmedEmail = true;
            //}).AddApiEndpoints()
            //    .AddEntityFrameworkStores<UserAccountDbContext>()
            //    .AddDefaultTokenProviders();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
