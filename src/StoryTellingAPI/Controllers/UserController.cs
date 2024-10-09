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
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        [Authorize("AdminAcces")]
        [HttpGet]
        public async Task<PaginatedItems<UserDisplay>> GetAsync([FromQuery] GetUserQueries getUserQueries)
        {
            string[] scopeRequiredByApi = ["access_as_user", "access_as_admin"];
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await Mediator.Send(getUserQueries);
        }

        [Authorize("ReadContent")]
        [HttpGet("{user_id}")]
        public async Task<UserDisplay> GetByIdAsync(string user_id)
        {
            return await Mediator.Send(new GetUserByIdQueries(user_id));
        }

        [Authorize("ReadContent")]
        [HttpGet("Own")]
        public async Task<UserDisplay> GetByIdAsync()
        {
            return await Mediator.Send(new GetUserByOwnIdQueries());
        }

        [Authorize("ReadContent")]
        [HttpGet("Simple/{user_id}")]
        public async Task<UserSimpleInfoDto> GetByIdAsync2(string user_id)
        {
            return await Mediator.Send(new GetUserSimpleInfoQueries(user_id));
        }

  
        [HttpGet("GenerateId/{username}")]
        public async Task<UserIdDto> GetAsync(string username)
        {
            return await Mediator.Send(new GetUserIDQueries(username));
        }


        [HttpPost]
        public async Task<ActionResult<Result>> CreateAsync(CreateUserCommand createUserCommand)
        {
            return await Mediator.Send(createUserCommand);
        }



        [Authorize("readContent")]
        [HttpPut]
        public async Task<ActionResult<Result>> UpdateAsync(UpdateUserCommand updateUserCommand)//modified
        {
            return  await Mediator.Send(updateUserCommand);
        }

        [Authorize("readContent")]
        [HttpPut("avatar")]
        public async Task<ActionResult<Result>> UpdateAsync(ModifyAvatarCommand updateUserCommand)//modified
        {
            return await Mediator.Send(updateUserCommand);
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
