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
        [Authorize("UserAccess")]
        [HttpGet]
        public async Task<PaginatedItems<StoryBoxesDto>> Get([FromQuery] GetStoryBoxByUserQueries getStoryTellingQueries)
        {
            return await Mediator.Send(getStoryTellingQueries);
        }

   
        [Authorize("UserAccess")]
        [HttpGet("{id}")]
        public async Task<StoryBoxesDto> Get(int id)
        {
            return await Mediator.Send(new GetStoryBoxByIdQueries(id));
        }

        [Authorize("UserAccess")]
        [HttpGet("facade")]
        public async Task<PaginatedItems<ChapterFacadeDto>> Get([FromQuery]GetChapitresFacadeQueries getChapitresFacade)
        {
            return await Mediator.Send(getChapitresFacade);
        }

        [Authorize("UserAccess")]
        [HttpGet("read")]
        public async Task<ChapterDto> Get([FromQuery]GetStoryBoxeReadQueries getStoryBoxeRead)
        {
            return await Mediator.Send(getStoryBoxeRead);
        }

        [Authorize("UserAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateStoryBoxAfterTransactionCommand createStoryTelling)//modified
        {
            return await Mediator.Send(createStoryTelling);
        }

        //native
        [Authorize("UserAccess")]
        [HttpPut("Stop-Reading/{id}/{order}")]
        public async Task<ActionResult<Result>> Put(int id,  int order)
        {

            return await Mediator.Send(new StopReadingBookCommand(id, order));
        }

     
    }
}
