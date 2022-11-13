using Application.Common.Behaviours;
using Application.Common.Interfaces;
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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviours<,>));
            services.AddScoped<IAuthorizationHandler, HasScopeHandler>();
            services.AddScoped<IReadOnlyPolicyRegistry<string>>(s => PolicyRegistries.GetRegistries());
            services.AddHttpClient<IImageSearchClient, HttpClientConfigure>(opt =>
            {
                opt.DefaultRequestHeaders.Add("X-RapidAPI-Host", "contextualwebsearch-websearch-v1.p.rapidapi.com");
                opt.DefaultRequestHeaders.Add("X-RapidAPI-Key", "c19b5fc326msh13c0592f3f85e79p1d62abjsn4e14621bbb92");
            });

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
                                                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:users"));
                    //       policy.Requirements
                    //.Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:role_members"));

                });
                     
                //author access
                options.AddPolicy("ReadAuthorAccess", policy => policy.Requirements
                                                                .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:Author-item")));
                options.AddPolicy("WriteAuthorAccess", policy => policy.Requirements
                                                                 .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "write:Author-item")));
                options.AddPolicy("UpdateAuthorAccess", policy => policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "update:Author-item")));
                options.AddPolicy("DeleteAuthorAccess", policy => policy.Requirements
                                                                  .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:Author-item")));
                options.AddPolicy("ForumAccess", policy => {
                                                        policy.Requirements
                                                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "create:forum"));
                                                        policy.Requirements
                                                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:forum"));
                });

                //user access
                options.AddPolicy("UpdateUserAccess", policy => policy.Requirements
                                                                .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "update:user-item")));
                options.AddPolicy("DeleteUserAccess", policy => policy.Requirements
                                                                .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "delete:user-item")));

                options.AddPolicy("ReadBookAccess", policy => policy.Requirements
                                                              .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:book")));
               
                options.AddPolicy("BuybookAccess", policy => policy.Requirements
                                                             .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "buy:book")));

                options.AddPolicy("BuyForfaitAccess", policy => policy.Requirements
                                                                .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "buy:forfait")));
              
                options.AddPolicy("Resilience", policy => policy.Requirements
                                                          .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "stop:forfait")));// a voir


                //userIdentity
                options.AddPolicy("ReadIdentityUser", policy => {
                                                        policy.Requirements
                                                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:users"));
                                                        policy.Requirements
                                                        .Add(new HasScopeRequirement(configuration["Auth0:Authority"], "read:user_idp_tokens"));
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
