using Microsoft.AspNetCore.Http;
using Application.Common.Models;
using Application.Ideas.Command.CreateCommand;
using Application.Ideas.Command.DeleteCommand;
using Application.Ideas.Command.UpdateCommand;
using Application.Ideas.Queries;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class IdeaController : ApiController
    {

        [HttpGet]
        public async Task<ICollection<IdeaDto>> GetIdeasByStory([FromQuery] GetIdeaByStoryTellIdQueries getIdeaByStoryTell)
        {
            return await Mediator.Send(getIdeaByStoryTell);
        }
        [HttpGet("{id}")]
        public async Task<IdeaDto> GetIdeaById(int id)
        {
            return await Mediator.Send(new GetIdeaByIdQueries(id));
        }
        [HttpPost]
        public async Task<Result> Post(CreateIdeaCommand createForfait)
        {
            return await Mediator.Send(createForfait);
        }
        [HttpPut]
        public async Task<Result> Update(UpdateIdeaCommand updateForfait)
        {
            return await Mediator.Send(updateForfait);
        }
        [HttpDelete]
        public async Task<Result> Delete([FromQuery] DeleteIdeaCommand deleteForfait)
        {
            return await Mediator.Send(deleteForfait);
        }
    }
}
