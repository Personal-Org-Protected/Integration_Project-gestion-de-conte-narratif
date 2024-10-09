using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{


    public class HasScopeRequirement:IAuthorizationRequirement
    {
        public string Issuer { get; } = "no Issuer";
        public string Scope { get; }

        public HasScopeRequirement(string issuer, string scope)
        {
            //Issuer = issuer ??throw new ArgumentNullException(nameof(issuer));
            Scope = scope ??throw new ArgumentNullException(nameof(scope)); ;
        }
    }


    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {

            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
            { context.Fail(); return Task.FromResult(HttpStatusCode.Unauthorized); }

            var permissions = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer).ToList();
            var scopes = permissions.Find(c => c.Value.Split(' ').Contains(requirement.Scope));
            if (scopes != null)
            { context.Succeed(requirement); return Task.CompletedTask; }

            context.Fail();
            return Task.FromResult(context.FailureReasons);
        }
    }


}
