using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Identity;
using Infrastructure.Identity.UserAuth0Client;
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
            var sqlString = new SqlConnectionStringBuilder(configuration.GetConnectionString("ConnectionSecurity"))
            {
                Password=configuration.GetSection("StoryTell_DB_Password").Value
            }.ConnectionString;
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        sqlString,
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());//avant c'etait scoped
            services.AddHttpContextAccessor();
           // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<IAuth0Client<UserCreate>, Auth0Client<UserCreate>>();
            services.AddHttpClient<IAuth0Client<UserUpdate>, Auth0Client<UserUpdate>>();
            services.AddHttpClient<ITokenAuth0Client, TokenAuth0Client>();
            return services;
        }
    }
}
