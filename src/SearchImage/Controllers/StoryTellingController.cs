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
        [Authorize("ReadContent")]//il faut changer
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
        [HttpGet("hasBeenBought/{id}")]
        public async Task<HasBeenBoughtDto> Get2(int id)
        {
            return await Mediator.Send(new StoryHasBeenBoughtQueries(id));
        }

        [Authorize("UserAccess")]
        [HttpGet("AlreadyBought/{id}")]
        public async Task<ActionResult<HasBeenBoughtDto>> get(int id)
        {
            return await Mediator.Send(new StoryAlreadyBoughtByUserQueries(id));
        }


        [Authorize("Read-author")]
        [HttpGet("user")]
        public async Task<PaginatedItems<StoryTellingDto>> Get([FromQuery] int pgNumber,[FromQuery] int idTag)//modified
        {
            return await Mediator.Send(new GetStoryByUserQueries(pgNumber,idTag));
        }


       [Authorize("UserAccess")]
        [HttpGet("Store")]
        public async Task<PaginatedItems<FacadeDto>> GetStore([FromQuery] StoreWindowQueries storeWindowQueries)//modified
        {
            return await Mediator.Send(storeWindowQueries);
        }

        //native
        [Authorize("ReadContent")]
        [HttpGet("facade")]
        public async Task<PaginatedItems<ChapterFacadeDto>> Get([FromQuery] GetChaptersFacadeQueries getChapitresFacade)
        {
            return await Mediator.Send(getChapitresFacade);
        }



        [Authorize("UserAccess")]
        [HttpGet("facade/{id}")]
        public async Task<FacadeDto> Get1(int id)//modified
        {
            return await Mediator.Send(new GetStoryTellFacadeByIdQueries(id));
        }

        [Authorize("AuthorAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateStoryTellingCommand createStoryTelling)//modified
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


        [Authorize("UserAccess")]
        [HttpPut("haveBeenBought/{id}")]
        public async Task<ActionResult<Result>> Put(int id)
        {
            return await Mediator.Send(new StoryHaveBeenBoughtCommand(id));
        }

        [Authorize("UserAccess")]
        [HttpPut("Rating/{id}")]
        public async Task<ActionResult<Result>> Put2(int id)
        {
            return await Mediator.Send(new RateStoryTellingCommand(id));
        }


        [Authorize("Read-author")]
        [HttpPut("Finish/{id}")]
        public async Task<ActionResult<Result>> Put3(int id)
        {
            return await Mediator.Send(new FinishStoryCommand(id));
        }

        [Authorize("AuthorAccess")]
        [HttpPut("Ongoing/{id}")]
        public async Task<ActionResult<Result>> Put4(int id)
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
