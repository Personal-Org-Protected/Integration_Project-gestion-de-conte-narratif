using Application.Common.Models;
using Application.StoryTellings.Command.CreateCommand;
using Application.StoryTellings.Command.DeleteCommand;
using Application.StoryTellings.Command.UpdateCommand;
using Application.StoryTellings.Queries;
using Application.StoryTellings.Queries.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoryTellingController : ApiController
    {
        //[Authorize("ReadAccess")]
        [HttpGet]
        public async Task<PaginatedItems<StoryTellingDto>> Get([FromQuery] GetStoryTellingQueries getStoryTellingQueries)
        {
            return await Mediator.Send(getStoryTellingQueries);
        }
        [HttpGet("Store")]
        public async Task<PaginatedItems<StoryTellingDto>> GetStore([FromQuery] StoreWindowQueries storeWindowQueries)
        {
            return await Mediator.Send(storeWindowQueries);
        }
        [HttpGet("read-book")]
        public async Task<StoryTellBoxDto> GetStore([FromQuery] ReadStoryTellingQueries readStoryTellingQueries)
        {
            return await Mediator.Send(readStoryTellingQueries);
        }
        //[Authorize("WriteAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateStoryTellingCommand createStoryTelling)
        {
            return await Mediator.Send(createStoryTelling);
        }
       // [Authorize("WriteAccess")]
        [HttpPut]
        public async Task<ActionResult<Result>> Put(UpdateStoryTellingCommand updateStoryTelling)
        {
            return await Mediator.Send(updateStoryTelling);
        }
        [HttpPut("Stop-Reading")]
        public async Task<ActionResult<Result>> Put(StopReadingBookCommand stopReadingBookCommand)
        {
            return await Mediator.Send(stopReadingBookCommand);
        }
        // [Authorize("DeleteAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteStoryTellingCommand(id));
        }

    }
}
