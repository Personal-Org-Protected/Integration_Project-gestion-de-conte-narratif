using Application.Common.Models;
using Application.UserCases.Author.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("AuthorAccess")]
    [ApiController]
    public class UseCaseAuthorController : ApiController
    {

        [HttpPost("image")]
        public async Task<ActionResult<Result>> PostResiliserAsync(UseCase_CreationImage useCase_Creation)//modified
        {
            var result= await Mediator.Send(useCase_Creation);
            //await Mediator.Publish(result);
            return result;
        }

        [HttpPost("chapitre")]
        public async Task<ActionResult<Result>> PostResiliserAsync(UseCase_CreationChapitre useCase_Creation)//modified
        {
            return await Mediator.Send(useCase_Creation);
        }

        [HttpPost("histoire")]
        public async Task<ActionResult<Result>> PostResiliserAsync(UseCase_CreationHistoire useCase_Creation)//modified
        {
            return await Mediator.Send(useCase_Creation);
        }
    }
}
