using Application.Common.Models;
using Application.StoryBox.Command.CreateCommand;
using Application.StoryBox.Command.UpdateCommand;
using Application.StoryBox.Query;
using Application.StoryBox.Query.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{

    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoryBoxController : ApiController
    {
        //native
        [Authorize("ReadContent")]
        [HttpGet]
        public async Task<PaginatedItems<StoryBoxesDto>> Get([FromQuery] GetStoryBoxByUserQueries getStoryTellingQueries)
        {
            return await Mediator.Send(getStoryTellingQueries);
        }

   
        [Authorize("ReadContent")]
        [HttpGet("{id}")]
        public async Task<StoryBoxesDto> Get(int id)
        {
            return await Mediator.Send(new GetStoryBoxByIdQueries(id));
        }

        [Authorize("ReadContent")]
        [HttpGet("facade")]
        public async Task<PaginatedItems<ChapterFacadeDto>> Get([FromQuery]GetChapitresFacadeQueries getChapitresFacade)
        {
            return await Mediator.Send(getChapitresFacade);
        }

        [Authorize("ReadContent")]
        [HttpGet("read")]
        public async Task<ChapterDto> Get([FromQuery]GetStoryBoxeReadQueries getStoryBoxeRead)
        {
            return await Mediator.Send(getStoryBoxeRead);
        }

        [Authorize("ReadContent")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateStoryBoxAfterTransactionCommand createStoryTelling)//modified
        {
            return await Mediator.Send(createStoryTelling);
        }

        //native
        [Authorize("ReadContent")]
        [HttpPut("Stop-Reading/{id}/{order}")]
        public async Task<ActionResult<Result>> Put(int id,  int order)
        {

            return await Mediator.Send(new StopReadingBookCommand(id, order));
        }

     
    }
}
