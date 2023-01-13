using Application.Common.Models;
using Application.Tags.Command.CreateCommand;
using Application.Tags.Command.DeleteCommand;
using Application.Tags.Command.UpdateCommand;
using Application.Tags.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TagController : ApiController
    {
        //[Authorize("ReadContent")]
        [HttpGet]
        public async Task<PaginatedItems<TagDto>> Get([FromQuery] GetTagQueries getTagQueries)
        {
            return await Mediator.Send(getTagQueries);
        }


        //[Authorize("ReadContent")]
        [HttpGet("{id}")]
        public async Task<TagDto> Get(int id)
        {
            return await Mediator.Send(new GetTagByIdQueries(id));
        }


        // [Authorize("AdminAcces")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateTagsCommand createTags)
        {
            return await Mediator.Send(createTags);
        }

        // [Authorize("AdminAcces")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Put(int id,UpdateTagCommand updateTags)
        {
            if (id != updateTags.IdTag)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateTags);
        }



        // [Authorize("AdminAcces")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteTagsCommand(id));
        }
    }
}
