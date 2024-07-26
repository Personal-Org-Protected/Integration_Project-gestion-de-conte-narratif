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
            services.AddScoped<IAuthorizationHandler, IsAdminHandler>();
            services.AddScoped<IReadOnlyPolicyRegistry<string>>(s => PolicyRegistries.GetRegistries());

            services.AddAuthorization(options => {

            //admin access
            options.AddPolicy("AdminAcces", policy => {
                policy.Requirements
                       .Add(new IsAdminRequirement(configuration["Azure:Issuer"]));
                //policy.Requirements
                //      .Add(new IsAdminRequirement(configuration["access_as_user"]));
            });

            //author access
            options.AddPolicy("AuthorAccess", policy => {//modified 
                        policy.Requirements
                           .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:Author-item"));
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "write:Author-item"));
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "update:Author-item"));
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "delete:Author-item"));

            });

            options.AddPolicy("Read-author", policy => {//modified
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:Author-item"));
            });


                //user access
                options.AddPolicy("UserAccess", policy => {
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "buy:book"));

            });

                options.AddPolicy("BasketAccess", policy => {
                    policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:basket"));
                    policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "write:basket"));
                    policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "delete:basket"));

                });


                options.AddPolicy("ReadContent", policy =>
                                                         {
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:item"));
                                                             policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "access_as_user"));//access_as_user
                                                         });


            //commentary Interactions
            options.AddPolicy("CommentaryAccess", policy => {
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "update:commentary"));
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "delete:commentary"));
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:commentary"));
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "create:commentary"));



                });


                options.AddPolicy("NotificationAccess", policy => {
                    policy.Requirements
                         .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "delete:notification"));
                    policy.Requirements
                         .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "update:notification"));

                });



                //forfait Intercations access
                options.AddPolicy("ForfaitAccess", policy =>
                {
                                                                policy.Requirements
                                                                   .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:forfait"));
                                                                policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "change:forfait"));

                });


                options.AddPolicy("RoleAccess", policy =>
                {
                                                                policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:role"));
                                                                policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "change:role"));

                });

                options.AddPolicy("Resilience", policy => {
                                                                    policy.Requirements
                                                                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "stop:forfait"));
                                                                    policy.Requirements
                                                                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "stop:role"));


                });// a voir


               // userIdentity
                options.AddPolicy("ReadIdentityUser", policy =>
                {
                                                                    policy.Requirements
                                                                            .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "read:users"));
                                                                    
                });


                options.AddPolicy("UpdateUser", policy => {
                                                        policy.Requirements
                                                        .Add(new HasScopeRequirement(configuration["Azure:Issuer"], "update:users"));
                     
                });
            });



            return services;
        }


    }
}
