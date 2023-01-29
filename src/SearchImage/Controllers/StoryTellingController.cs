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
        [Authorize("ReadContent")]
        [HttpGet]
        public async Task<PaginatedItems<StoryTellingDto>> Get([FromQuery] GetStoryTellingQueries getStoryTellingQueries)
        {
            return await Mediator.Send(getStoryTellingQueries);
        }


        [Authorize("ReadContent")]
        [HttpGet("{id}")]
        public async Task<StoryTellingDto> Get(int id)
        {
            return await Mediator.Send(new GetStoryTellByIdQueries(id));
        }

        [Authorize("ReadContent")]
        [HttpGet("hasBeenBought/{user_id}")]
        public async Task<HasBeenBoughtDto> Get(string user_id)
        {
            return await Mediator.Send(new StoryHasBeenBoughtQueries(user_id));
        }


        [Authorize("AuthorAccess")]
        [HttpGet("user/{user_id}")]
        public async Task<PaginatedItems<StoryTellingDto>> Get(string user_id,[FromQuery] int pgNumber)
        {
            return await Mediator.Send(new GetStoryByUserQueries(user_id,pgNumber));
        }


        [Authorize("ReadContent")]
        [HttpGet("Store")]
        public async Task<PaginatedItems<FacadeDto>> GetStore([FromQuery] StoreWindowQueries storeWindowQueries)
        {
            return await Mediator.Send(storeWindowQueries);
        }


        [Authorize("ReadContent")]
        [HttpGet("facade/{id}")]
        public async Task<FacadeDto> Get1(int id,[FromQuery] string user_id)
        {
            return await Mediator.Send(new GetStoryTellFacadeByIdQueries(id,user_id));
        }

        [Authorize("AuthorAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateStoryTellingCommand createStoryTelling)
        {
            return await Mediator.Send(createStoryTelling);
        }


        [Authorize("AuthorAccess")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Put(int id,UpdateStoryTellingCommand updateStoryTelling)
        {

            if (id != updateStoryTelling.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateStoryTelling);
        }


        [Authorize("AuthorAccess")]
        [HttpPut("haveBeenBought/{id}")]
        public async Task<ActionResult<Result>> Put(int id)
        {
            return await Mediator.Send(new StoryHaveBeenBoughtCommand(id));
        }


        [Authorize("AuthorAccess")]
        [HttpPut("Finish/{id}")]
        public async Task<ActionResult<Result>> Put2(int id)
        {
            return await Mediator.Send(new FinishStoryCommand(id));
        }

        [Authorize("AuthorAccess")]
        [HttpPut("Ongoing/{id}")]
        public async Task<ActionResult<Result>> Put3(int id)
        {
            return await Mediator.Send(new OngoingStoryCommand(id));
        }

        [Authorize("AuthorAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteStoryTellingCommand(id));
        }

    }
}
