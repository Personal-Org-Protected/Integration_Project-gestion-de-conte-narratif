using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Methods;
using Application.Common.RetryPolicies;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.DependencyInjections
{
    public static class Dependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddHttpContextAccessor();
            services.AddScoped<IUser, UserInfo>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviours<,>));
            services.AddScoped<IAuthorizationHandler, HasScopeHandler>();
            services.AddScoped<IReadOnlyPolicyRegistry<string>>(s => PolicyRegistries.GetRegistries());

            services.AddAuthorization(options => {

                //admin access
            options.AddPolicy("AdminAcces", policy => {
                policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:item-admin"));
                policy.Requirements
                       .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "create:item-admin"));
                policy.Requirements
                       .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:item-admin"));
            });


            //author access
            options.AddPolicy("AuthorAccess", policy => {//modified 
                    policy.Requirements
                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:author-item"));
                    policy.Requirements
                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "write:author-item"));
                    policy.Requirements
                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:author-item"));
            });


            options.AddPolicy("ReadContent", policy =>
                                                        {
                    policy.Requirements
                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:item"));
                                                            policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "write:item"));
                                                            policy.Requirements
                     .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:item"));
                                                        });
            });



            return services;
        }


    }
}
