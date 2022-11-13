using Application.Common.Models;
using Application.Tags.Command.CreateCommand;
using Application.Tags.Command.DeleteCommand;
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
        //[Authorize("ReadAccess")]
        [HttpGet]
        public async Task<TagVM> Get()
        {
            return await Mediator.Send(new GetTagQueries());
        }
       // [Authorize("WriteAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateTagsCommand createTags)
        {
            return await Mediator.Send(createTags);
        }
        //[Authorize("DeleteAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteTagsCommand(id));
        }
    }
}
