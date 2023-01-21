using Application.Common.Models;
using Application.UserRoles.Command.CreateCommand;
using Application.UserRoles.Command.DeleteCommand;
using Application.UserRoles.Query;
using Application.UsersAuth.Command.CreateCommand.CreateUserAuthCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserRolesController : ApiController
    {

        [Authorize("AdminAcces")]
        [HttpGet("{user_id}")]
        public async Task<UserRolesVM> GetAsync(string user_id)
        {
            return await Mediator.Send(new GetRolesOfUserQueries(user_id));
        }

        [HttpPost("Default")]
        public async Task<ActionResult<Result>> CreateAsync(CreateDefaultRoleForUserCommand createDefaultRoleForUserCommand)
        {
            return await Mediator.Send(createDefaultRoleForUserCommand);
        }

        [Authorize("RoleAccess")]
        [HttpPost("Author")]
        public async Task<ActionResult<Result>> CreateAsync(AddAuthorRoleForUserCommand addAuthorRoleForUserCommand)
        {
            return await Mediator.Send(addAuthorRoleForUserCommand);
        }

        [Authorize("RoleAccess")]
        [HttpPost("FormerAuthor")]
        public async Task<ActionResult<Result>> CreateAsync(AddFormerAuthorRoleForUserCommand addFormerAuthorRoleForUserCommand)
        {
            return await Mediator.Send(addFormerAuthorRoleForUserCommand);
        }



        [Authorize("Resilience")]
        [HttpDelete("Resiliate/{user_id}")]
        public async Task<ActionResult<Result>> DeleteAsync(string user_id,[FromQuery]int roleId)
        {
            return await Mediator.Send(new DeleteUserRoleCommand(user_id, roleId));
        }


        //[HttpDelete("Resiliate/Author/{user_id}")]
        //public async Task<ActionResult<Result>> DeleteAsync2(string user_id)
        //{
        //    return await Mediator.Send(new ResiliateAuthorRoleCommand(user_id));
        //}


        //[HttpDelete("Resiliate/FormerAuthor/{user_id}")]
        //public async Task<ActionResult<Result>> DeleteAsync3(string user_id)
        //{
        //    return await Mediator.Send(new ResiliateFormerAuthorRoleCommand(user_id));
        //}
    }
}
