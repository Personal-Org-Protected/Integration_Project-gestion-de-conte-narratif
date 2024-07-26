using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class IsAdminBehaviours : IAuthorizationRequirement
    {
       
    }
    public class IsAdminRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }

        public IsAdminRequirement(string issuer)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }

    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (requirement == null)
                throw new ArgumentNullException(nameof(requirement));

            var claimIdentityprovider = context.User.Claims.FirstOrDefault(t => t.Type == "idp");

            // check that our tenant was used to signin
            if (claimIdentityprovider != null
                && claimIdentityprovider.Value ==
                    "https://login.microsoftonline.com/bc284a57-173f-4482-8248-5adf6f281fe1/v2.0" && context.User.HasClaim(c=>c.Issuer == requirement.Issuer))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
