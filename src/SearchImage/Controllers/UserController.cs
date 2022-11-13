using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Command.CreateCommand;
using Application.Users.Command.DeleteCommand;
using Application.Users.Command.UpdateCommand;
using Application.Users.Query;
using Application.Users.Query.Dto_s;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        //[Authorize("ReadUser")]
        [HttpGet]
        public async Task<PaginatedItems<UserDisplay>> GetAsync([FromQuery] GetUserQueries getUserQueries)
        {
            return await Mediator.Send(getUserQueries);
        }
       // [Authorize("ReadUser")]
        [HttpGet("{user_id}")]
        public async Task<UserDisplay> GetByIdAsync(string user_id)
        {
            return await Mediator.Send(new GetUserByIdQueries(user_id));
        }
        [HttpPost]
        public async Task<Result> CreateAsync(CreateUserCommand createUserCommand)
        {
            return await Mediator.Send(createUserCommand);
        }
       // [Authorize("UpdateUser")]
        [HttpPut("{user_id}")]
        public async Task<Result> UpdateAsync(UpdateUserCommand updateUserCommand)
        {
          return  await Mediator.Send(updateUserCommand);
        }
        //[Authorize("AdminAccess")]
        [HttpDelete("{user_id}")]
        public async Task<Result> DeleteAsync(string user_id)
        {
            return await Mediator.Send(new DeleteUserCommand(user_id));
        }

        [HttpPut("Choice-Forfait")]
        public async Task<Result> ChoiceForfait([FromQuery]ChoiceForfaitCommand choiceForfaitCommand)
        {
            return await Mediator.Send(choiceForfaitCommand);
        }

        [HttpPut("Resilience-Forfait")]
        public async Task<Result> ResilieForfait([FromQuery]ResiliateForfeitCommand resiliateForfeit)
        {
            return await Mediator.Send(resiliateForfeit);
        }

        [HttpGet("UsersIntern")]
        public async Task<PaginatedItems<UserInternDto>> GetInternAsync([FromQuery] GetUserInternQueries getUserQueries)
        {
            return await Mediator.Send(getUserQueries);
        }
        [HttpGet("UsersIntern/{user_id}")]
        public async Task<UserInternDto> GetInternAsyncById([FromQuery] GetUserInternByIdentityUserIdQueries getUserQueriesById)
        {
            return await Mediator.Send(getUserQueriesById);
        }
    }
}
