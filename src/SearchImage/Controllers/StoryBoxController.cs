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
    [Authorize("UserAccess")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoryBoxController : ApiController
    {

 
        [HttpGet]
        public async Task<PaginatedItems<StoryBoxesDto>> Get([FromQuery] GetStoryBoxByUserQueries getStoryTellingQueries)
        {
            return await Mediator.Send(getStoryTellingQueries);
        }


        [HttpGet("{id}")]
        public async Task<StoryBoxesDto> Get(int id)
        {
            return await Mediator.Send(new GetStoryBoxByIdQueries(id));
        }


        [HttpGet("facade")]
        public async Task<PaginatedItems<ChapterFacadeDto>> Get([FromQuery]GetChapitresFacadeQueries getChapitresFacade)
        {
            return await Mediator.Send(getChapitresFacade);
        }


        [HttpGet("read")]
        public async Task<ChapterDto> Get([FromQuery]GetStoryBoxeReadQueries getStoryBoxeRead)
        {
            return await Mediator.Send(getStoryBoxeRead);
        }


        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateStoryBoxAfterTransactionCommand createStoryTelling)
        {
            return await Mediator.Send(createStoryTelling);
        }


        [HttpPut("Stop-Reading/{id}/{order}")]
        public async Task<ActionResult<Result>> Put(int id,  int order)
        {

            return await Mediator.Send(new StopReadingBookCommand(id, order));
        }
    }
}
