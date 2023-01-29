using Application.Common.Models;
using Application.UsersAuth.Command.CreateCommand;
using Application.UsersAuth.Command.CreateCommand.CreateUserAuthCommand;
using Application.UsersAuth.Command.DeleteCommand;
using Application.UsersAuth.Command.UpdateCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserAuthController : ApiController
    {

        [HttpPost]
        public async Task<ActionResult<Result>> CreateAsync(CreateUserAuthCommand createUserAuthCommand)
        {
            return await Mediator.Send(createUserAuthCommand);
        }

        [HttpPost("Default")]
        public async Task<ActionResult<Result>> CreateAsync(ConfigurationDefaultRole configurationDefaultRole)
        {
            return await Mediator.Send(configurationDefaultRole);
        }

        [Authorize("RoleAccess")]
        [HttpPost("Author")]
        public async Task<ActionResult<Result>> CreateAsync(AddAuthorRoleAuthUserCommand addAuthorRoleAuthUserCommand)
        {
            return await Mediator.Send(addAuthorRoleAuthUserCommand);
        }

        [Authorize("RoleAccess")]
        [HttpPost("FormerAuthor")]
        public async Task<ActionResult<Result>> CreateAsync(AddFormerAuthorRoleAuthUserCommand addFormerAuthorRoleAuthUserCommand)
        {
            return await Mediator.Send(addFormerAuthorRoleAuthUserCommand);
        }


        [Authorize("RoleAccess")]
        [HttpPost("Admin")]
        public async Task<ActionResult<Result>> CreateAsync(AddAdminRoleAuthUserCommand adminRoleAuthUserCommand)
        {
            return await Mediator.Send(adminRoleAuthUserCommand);
        }


        [Authorize("UpdateUser")]
        [HttpPut("{user_id}")]
        public async Task<ActionResult<Result>> UpdateAsync(string user_id,UpdateUserAuthCommand updateUserAuthCommand)
        {
            if (user_id != updateUserAuthCommand.user_id)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateUserAuthCommand);
        }

        [Authorize("UpdateUser")]
        [HttpPut("block/{user_id}")]
        public async Task<ActionResult<Result>> UpdateAsync(string user_id, BlockUserAuthCommand updateUserAuthCommand)
        {
            if (user_id != updateUserAuthCommand.user_id)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateUserAuthCommand);
        }

        [Authorize("AdminAcces")]
        [HttpDelete("User/{user_id}")]
        public async Task<ActionResult<Result>> DeleteAsync(string user_id)
        {
            return await Mediator.Send(new DeleteUserAuthCommand(user_id));
        }

        [Authorize("Resilience")]
        [HttpDelete("Resiliate/{user_id}")]
        public async Task<ActionResult<Result>> DeleteAsync2(string user_id,[FromQuery]int roleId)
        {
            return await Mediator.Send(new ResiliateAuthRoleCommand(user_id, roleId));
        }

        //[HttpDelete("FormerAuthor/{user_id}")]
        //public async Task<ActionResult<Result>> DeleteAsync3(string user_id)
        //{
        //    return await Mediator.Send(new ResiliateFormerAuthorAuthRoleCommand(user_id));
        //}
    }
}
