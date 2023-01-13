using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Stories.Command.CreateCommand;
using Application.Stories.Command.DeleteCommand;
using Application.Stories.Command.UpdateCommand;
using Application.Stories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoryController : ApiController
    {

        // [Authorize("ReadContent")]
        [HttpGet("{id}")]
        public async Task<StoryDto> GetById(int id)
        {
            return await Mediator.Send(new GetStoriesByIdQueries(id));
        }
        //[Authorize("AuthorAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateStoriesCommand createStories)
        {
            return await Mediator.Send(createStories);
        }
        //[Authorize("AuthorAccess")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Put(int id, UpdateStoriesCommand updateStories)
        {
            if (id != updateStories.IdStory)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateStories);
        }
        //[Authorize("AuthorAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteStoriesCommand(id));
        }

    }
}
