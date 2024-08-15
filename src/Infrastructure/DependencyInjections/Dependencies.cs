﻿using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjections
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string sqlString = "Server=WP4163;Database=StoryTelling;User Id=StoryTell_user;Password=Mouhsine1998*;Integrated Security=true;Trusted_Connection=True;TrustServerCertificate=True";
        //    var sqlString = new SqlConnectionStringBuilder(configuration.GetConnectionString("ConnectionSecurity"))
        //    {
        //        Password= Environment.GetEnvironmentVariable("StoryTell_DB_Password")
        //}.ConnectionString;
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        sqlString,
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());//avant c'etait scoped
            services.AddHttpContextAccessor();
           // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
