using Application.Commentaries.Command.CreateCommand;
using Application.Commentaries.Command.DeleteCommand;
using Application.Commentaries.Command.UpdateCommand;
using Application.Commentaries.Queries;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CommentaryController : ApiController
    {
        [HttpGet]
        public async Task<PaginatedItems<CommentaryDto>> GetByStories([FromQuery] GetCommentariesByStoryTellQueries getCommentaries)
        {
            return await Mediator.Send(getCommentaries);
        }
        [HttpGet("Signal")]
        public async Task<PaginatedItems<CommentaryDto>> GetSignaledComm([FromQuery] GetSignaledCommentariesQueries getCommentaries)
        {
            return await Mediator.Send(getCommentaries);
        }

        [HttpPost]
        public async Task<Result> Post(CreateCommentaryCommand createCommentary)
        {
            return await Mediator.Send(createCommentary);
        }
        [HttpPut("Signal")]
        public async Task<Result> SignalCommentary([FromQuery]SignalCommentaryCommand signalCommentary)
        {
            return await Mediator.Send(signalCommentary);
        }
        [HttpPut("Signal-to-zero")]
        public async Task<Result> ZeroSignalCommentary([FromQuery] SignalCommBackToZeroCommand signalCommentary)
        {
            return await Mediator.Send(signalCommentary);
        }
        [HttpDelete]
        public async Task<Result> Delete([FromQuery] DeleteCommentaryCommand deleteCommentary)
        {
            return await Mediator.Send(deleteCommentary);
        }
    }
}
