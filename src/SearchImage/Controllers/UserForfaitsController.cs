using Application.Common.Models;
using Application.UserForfaits.Command.CreateCommand;
using Application.UserForfaits.Command.DeleteCommand;
using Application.UserForfaits.Command.UpdateCommand;
using Application.UserForfaits.Query;
using Application.UserForfaits.Query.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserForfaitsController : ApiController
    {


        [Authorize("AdminAcces")]//modified
        [HttpGet("{user_id}")]
        public async Task<UserForfaitVM> GetAsync(string user_id)
        {
            return await Mediator.Send(new GetForfaitOfUserQueries(user_id));
        }


   

        [Authorize("ForfaitAccess")]
        [HttpGet]
        public async Task<UserForfaitVM> GetAsync()//modified
        {
            return await Mediator.Send(new GetOwnUserForfaitsQueries());
        }


        [HttpGet("HaveThisForfait/{user_id}")]
        public async Task<HaveForfaitDto> GetAsync(int idForfait)//modified
        {
            return await Mediator.Send(new AlreadyHaveThisForfaitQueries(idForfait));
        }


        [HttpGet("HaveForfaitLambda")]
        public async Task<HaveForfaitDto> GetAsync3()//modified
        {
            return await Mediator.Send(new AlreadyHaveForfaitLambdaQueries());
        }

        [HttpGet("HaveForfaitAuthor")]
        public async Task<HaveForfaitDto> GetAsync2()//modified
        {
            return await Mediator.Send(new AlreadyHaveForfaitAuthorQueries());
        }


        [HttpPost("Default")]
        public async Task<ActionResult<Result>> CreateAsync(CreateForfaitForDefaultUserCommand createForfaitForDefaultUserCommand )
        {
            return await Mediator.Send(createForfaitForDefaultUserCommand);
        }

        [Authorize("ForfaitAccess")]
        [HttpPost("Standard")]
        public async Task<ActionResult<Result>> CreateAsync(AddForfaitCommand addForfaitLambdaCommand)//modified
        {
            return await Mediator.Send(addForfaitLambdaCommand);
        }


        //[HttpPut]
        //public async Task<ActionResult<Result>> UpdateAsync(ChangeForfaitCommand changeForfaitCommand)
        //{
        //    return await Mediator.Send(changeForfaitCommand);
        //}

        [Authorize("AdminAcces")]
        [HttpDelete("{idForfait}")]
        public async Task<ActionResult<Result>> DeleteAdminAsync(int idForfait)
        {
            return await Mediator.Send(new DeleteUserForfait(idForfait));
        }

        [Authorize("Resilience")]
        [HttpDelete("Resiliate/{idForfait}")]
        public async Task<ActionResult<Result>> DeleteAsync(int idForfait)//modified
        {
            return await Mediator.Send(new ResiliateForfaitCommand(idForfait));
        }

    }
}
