using Application.Common.Models;
using Application.UserCases.Admin.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UseCaseAdminController : ApiController
    {

        [HttpDelete("{user_id}")]
        public async Task<ActionResult<Result>> PostResiliserAsync(string user_id)//modified
        {
            return await Mediator.Send(new UseCase_SupprimerUtilisateur(user_id));
        }

    }
}
