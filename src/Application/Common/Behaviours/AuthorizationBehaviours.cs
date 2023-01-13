﻿using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{


    public class HasScopeRequirement:IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasScopeRequirement(string issuer, string scope)
        {
            Issuer = issuer??throw new ArgumentNullException(nameof(issuer));
            Scope = scope??throw new ArgumentNullException(nameof(scope)); ;
        }
    }


    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {//a voir si durant une raquette lid du use est passe aussi
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                throw new ForbiddenAccessException("no scope found");

            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Value.Split(' ');

            if (scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }


}
