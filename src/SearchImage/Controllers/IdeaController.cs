using Microsoft.AspNetCore.Http;
using Application.Common.Models;
using Application.Ideas.Command.CreateCommand;
using Application.Ideas.Command.DeleteCommand;
using Application.Ideas.Command.UpdateCommand;
using Application.Ideas.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SearchImage.Controllers
{
   // [Authorize("AuthorAccess")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class IdeaController : ApiController
    {

        [HttpGet("StoryTell/{id}")]
        public async Task<IdeaVM> GetIdeasByStory(int id)
        {
            return await Mediator.Send(new GetIdeaByStoryTellIdQueries(id));
        }
        [HttpGet("{id}")]
        public async Task<IdeaDto> GetIdeaById(int id)
        {
            return await Mediator.Send(new GetIdeaByIdQueries(id));
        }
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateIdeaCommand createForfait)
        {
            return await Mediator.Send(createForfait);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id,UpdateIdeaCommand updateForfait)
        {
            if (id != updateForfait.idIdea)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateForfait);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteIdeaCommand(id));
        }
        [HttpDelete("histoire/{id}")]
        public async Task<ActionResult<Result>> Delete2(int id)
        {
            return await Mediator.Send(new DeleteAllIdeaCommand(id));
        }
    }
}
