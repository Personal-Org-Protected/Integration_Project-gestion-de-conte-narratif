using Application.Common.Models;
using Application.Forfaits.Command.CreateCommand;
using Application.Forfaits.Command.DeleteCommand;
using Application.Forfaits.Command.UpdateCommand;
using Application.Forfaits.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ForfaitController :ApiController
    {
        [HttpGet("User-Forfait")]
        public async Task<PaginatedItems<ForfaitDto>> GetForUser([FromQuery] GetForfaitUserQueries getForfaitUser)
        {
            return await Mediator.Send(getForfaitUser);
        }
        [HttpGet("Author-Forfait")]
        public async Task<PaginatedItems<ForfaitDto>> GetForAuthor([FromQuery] GetForfaitAuthorQueries getForfaitAuthor)
        {
            return await Mediator.Send(getForfaitAuthor);
        }
        [HttpPost]
        public async Task<Result> Post(CreateForfaitCommand createForfait)
        {
            return await Mediator.Send(createForfait);
        }
        [HttpPut]
        public async Task<Result> Update(UpdateForfaitCommand updateForfait)
        {
            return await Mediator.Send(updateForfait);
        }
        [HttpDelete]
        public async Task<Result> Delete([FromQuery] DeleteUserLambdaForfaitCommand deleteForfait)
        {
            return await Mediator.Send(deleteForfait);
        }
    }
}
