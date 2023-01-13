using Application.Commentaries.Command.CreateCommand;
using Application.Commentaries.Command.DeleteCommand;
using Application.Commentaries.Command.UpdateCommand;
using Application.Commentaries.Queries;
using Application.Commentaries.Queries.Dto;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    //[Authorize("CommentaryAccess")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CommentaryController : ApiController
    {
       // 
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




        [HttpGet("user/{user_id}")]
        public async Task<CommentaryVM> GetSignaledComm(string user_id)
        {
            return await Mediator.Send(new GetCommentairesByUserQueries(user_id));
        }


        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateCommentaryCommand createCommentary)
        {
            return await Mediator.Send(createCommentary);
        }



        [HttpPut("Like/{id}")]
        public async Task<ActionResult<Result>> LikeCommentary(int id)
        {
            return await Mediator.Send(new LikeCommentaryCommand(id));
        }


        [HttpPut("Signal/{id}")]
        public async Task<ActionResult<Result>> SignalCommentary(int id)
        {
            return await Mediator.Send(new SignalCommentaryCommand(id));
        }



        [HttpPut("Signal-to-zero/{id}")]
        public async Task<ActionResult<Result>> ZeroSignalCommentary(int id)
        {
            return await Mediator.Send(new SignalCommBackToZeroCommand (id));
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteCommentaryCommand(id));
        }



        [HttpDelete("user/{user_id}")]
        public async Task<ActionResult<Result>> Delete(string user_id)
        {
            return await Mediator.Send(new DeleteAllCommentaryByUserCommand(user_id));
        }
    }
}
