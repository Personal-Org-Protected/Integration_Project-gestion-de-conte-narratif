using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Methods;
using Application.Common.RetryPolicies;
using Application.ImagesClient;
using Application.ImagesClient.Queries;
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
            services.AddMediatR(Assembly.GetExecutingAssembly());
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
                policy.Requirements
                     .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "write:notification"));

            });

            //author access
            options.AddPolicy("AuthorAccess", policy => {//modified 
                        policy.Requirements
                           .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:Author-item"));
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "write:Author-item"));
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "update:Author-item"));
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:Author-item"));

            });

            options.AddPolicy("Read-author", policy => {//modified
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:Author-item"));
            });


                //user access
                options.AddPolicy("UserAccess", policy => {
                        policy.Requirements
                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "buy:book"));

            });

                options.AddPolicy("BasketAccess", policy => {
                    policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:basket"));
                    policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "write:basket"));
                    policy.Requirements
                      .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:basket"));

                });


                options.AddPolicy("ReadContent", policy =>
                                                         {
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:item"));
                                                   });


            //commentary Interactions
            options.AddPolicy("CommentaryAccess", policy => {
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "update:commentary"));
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:commentary"));
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:commentary"));
                                                                policy.Requirements
                                                                    .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "create:commentary"));



                });


                options.AddPolicy("NotificationAccess", policy => {
                    policy.Requirements
                         .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:notification"));
                    policy.Requirements
                         .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "update:notification"));

                });



                //forfait Intercations access
                options.AddPolicy("ForfaitAccess", policy =>
                {
                                                                policy.Requirements
                                                                   .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:forfait"));
                                                                policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "change:forfait"));

                });


                options.AddPolicy("RoleAccess", policy =>
                {
                                                                policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:role"));
                                                                policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "change:role"));

                });

                options.AddPolicy("Resilience", policy => {
                                                                    policy.Requirements
                                                                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "stop:forfait"));
                                                                    policy.Requirements
                                                                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "stop:role"));


                });// a voir


               // userIdentity
                options.AddPolicy("ReadIdentityUser", policy =>
                {
                                                                    policy.Requirements
                                                                            .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:users"));
                                                                    
                });


                options.AddPolicy("UpdateUser", policy => {
                                                        policy.Requirements
                                                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "update:users"));
                     
                });
            });



            return services;
        }


    }
}
