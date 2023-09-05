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

        [Authorize("RoleAccess")]
        [HttpGet("{user_id}")]
        public async Task<UserRolesVM> GetAsync(string user_id)
        {
            return await Mediator.Send(new GetRolesOfUserQueries(user_id));
        }

        [Authorize("RoleAccess")]
        [HttpGet]
        public async Task<UserRolesVM> GetAsync()
        {
            return await Mediator.Send(new GetOwnRolesQueries());
        }

        [Authorize("AdminAcces")]
        [HttpGet("isAdmin/{user_id}")]
        public async Task<IsRoleDto> GetAdminRoleAsync(string user_id)
        {
            return await Mediator.Send(new UserIsAdminQueries(user_id));
        }

        [Authorize("RoleAccess")]
        [HttpGet("isAdmin")]
        public async Task<IsRoleDto> GetAdminRoleAsync()
        {
            return await Mediator.Send(new IsUserAdminQueries());
        }


        [Authorize("RoleAccess")]
        [HttpGet("isAuthor")]
        public async Task<IsRoleDto> GetAuthorRoleAsync()
        {
            return await Mediator.Send(new IsUserAuthorQueries());
        }

        [Authorize("RoleAccess")]
        [HttpGet("isFormer")]
        public async Task<IsRoleDto> GetFormerRoleAsync( )
        {
            return await Mediator.Send(new IsUserFormerQueries());
        }


        [HttpPost("Default")]
        public async Task<ActionResult<Result>> CreateAsync(CreateDefaultRoleForUserCommand createDefaultRoleForUserCommand)
        {
            return await Mediator.Send(createDefaultRoleForUserCommand);
        }

        [HttpPost("Role")]
        public async Task<ActionResult<Result>> CreateRoleAsync(AddOwnRoleCommand addOwnRole)
        {
            return await Mediator.Send(addOwnRole);
        }


        [Authorize("RoleAccess")]
        [HttpPost("Author")]
        public async Task<ActionResult<Result>> CreateAuthorAsync()//modified
        {
            return await Mediator.Send(new AddAuthorRoleForUserCommand());
        }

        [Authorize("RoleAccess")]
        [HttpPost("FormerAuthor")]
        public async Task<ActionResult<Result>> CreateFormerAsync()//modified
        {
            return await Mediator.Send(new AddFormerAuthorRoleForUserCommand());
        }

        [Authorize("AdminAcces")]
        [HttpPost("Admin")]
        public async Task<ActionResult<Result>> CreateAdminAsync(AddAdminRoleForUserCommand addAdminRoleForUser)
        {
            return await Mediator.Send(addAdminRoleForUser);
        }



        [Authorize("AdminAcces")]//modified
        [HttpDelete("Resiliate/{user_id}")]
        public async Task<ActionResult<Result>> DeleteAsync(string user_id,[FromQuery]int roleId)
        {
            return await Mediator.Send(new DeleteUserRoleCommand(user_id, roleId));
        }

        [Authorize("Resilience")]
        [HttpDelete("Resiliate/Own/{id}")]
        public async Task<ActionResult<Result>> DeleteAsync( int id)//modified
        {
            return await Mediator.Send(new DeleteOwnUserRoleCommand(id));
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
