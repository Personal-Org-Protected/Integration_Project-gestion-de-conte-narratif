using Application.Common.Models;
using Application.UserCases.Admin.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace SearchImage.Controllers
{
    [Authorize(AuthenticationSchemes = "BearerAdmin", Policy = "AdminAcces")]
    [AuthorizeForScopes(Scopes = ["api://0e2bb51f-3f7f-4760-a684-da2f6a4c17df/access_as_user"])]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UseCaseAdminController : ApiController
    {

        [HttpDelete("{user_id}")]
        public async Task<ActionResult<Result>> PostResiliserAsync(string user_id)//modified
        {
            string[] scopeRequiredByApi = ["access_as_user"];
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await Mediator.Send(new UseCase_SupprimerUtilisateur(user_id));
        }

    }
}
