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
        //[Authorize("AdminAcces")]
        [HttpGet]
        public async Task<PaginatedItems<UserDisplay>> GetAsync([FromQuery] GetUserQueries getUserQueries)
        {
            return await Mediator.Send(getUserQueries);
        }

        //[Authorize("AdminAcces")]
        [HttpGet("{user_id}")]
        public async Task<UserDisplay> GetByIdAsync(string user_id)
        {
            return await Mediator.Send(new GetUserByIdQueries(user_id));
        }

        //[Authorize("ReadContent")]
        [HttpGet("Simple/{user_id}")]
        public async Task<UserSimpleInfoDto> GetByIdAsync2(string user_id)
        {
            return await Mediator.Send(new GetUserSimpleInfoQueries(user_id));
        }

  
        [HttpGet("GenerateId")]
        public async Task<UserIdDto> GetAsync([FromQuery]GetUserIDQueries getUserIDQueries)
        {
            return await Mediator.Send(getUserIDQueries);
        }


        [HttpPost]
        public async Task<ActionResult<Result>> CreateAsync(CreateUserCommand createUserCommand)
        {
            return await Mediator.Send(createUserCommand);
        }



       // [Authorize("UpdateUser")]
        [HttpPut("{user_id}")]
        public async Task<ActionResult<Result>> UpdateAsync(string user_id, UpdateUserCommand updateUserCommand)
        {
            if (user_id != updateUserCommand.user_id)
            {
                return BadRequest();
            }
            return  await Mediator.Send(updateUserCommand);
        }




        //[Authorize("AdminAccess")]
        //[HttpDelete("{user_id}")]
        //public async Task<ActionResult<Result>> DeleteAsync(string user_id)
        //{
        //    return await Mediator.Send(new DeleteUserCommand(user_id));
        //}

        //[HttpPut("Choice-Forfait")]
        //public async Task<ActionResult<Result>> ChoiceForfait([FromQuery]ChoiceForfaitCommand choiceForfaitCommand)
        //{
        //    return await Mediator.Send(choiceForfaitCommand);
        //}

        //[HttpPut("Resilience-Forfait")]
        //public async Task<ActionResult<Result>> ResilieForfait([FromQuery]ResiliateForfeitCommand resiliateForfeit)
        //{
        //    return await Mediator.Send(resiliateForfeit);
        //}

        //[HttpGet("UsersIntern")]
        //public async Task<PaginatedItems<UserInternDto>> GetInternAsync([FromQuery] GetUserInternQueries getUserQueries)
        //{
        //    return await Mediator.Send(getUserQueries);
        //}
        //[HttpGet("UsersIntern/{user_id}")]
        //public async Task<UserInternDto> GetInternAsyncById(string user_id)
        //{
        //    return await Mediator.Send(new GetUserInternByIdentityUserIdQueries(user_id));
        //}
    }
}
