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

        //[Authorize("ForfaitAccess")]
        [HttpGet("{user_id}")]
        public async Task<UserForfaitVM> GetAsync(string user_id)
        {
            return await Mediator.Send(new GetForfaitOfUserQueries(user_id));
        }


        [HttpGet("HaveThisForfait/{user_id}")]
        public async Task<HaveForfaitDto> GetAsync(string user_id,int idForfait)
        {
            return await Mediator.Send(new AlreadyHaveThisForfaitQueries(user_id,idForfait));
        }


        [HttpGet("HaveForfaitLambda/{user_id}")]
        public async Task<HaveForfaitDto> GetAsync3(string user_id)
        {
            return await Mediator.Send(new AlreadyHaveForfaitLambdaQueries(user_id));
        }

        [HttpGet("HaveForfaitAuthor/{user_id}")]
        public async Task<HaveForfaitDto> GetAsync2(string user_id)
        {
            return await Mediator.Send(new AlreadyHaveForfaitAuthorQueries(user_id));
        }


        [HttpPost("Default")]
        public async Task<ActionResult<Result>> CreateAsync(CreateForfaitForDefaultUserCommand createForfaitForDefaultUserCommand )
        {
            return await Mediator.Send(createForfaitForDefaultUserCommand);
        }

        //[Authorize("ForfaitAccess")]
        [HttpPost("Standard")]
        public async Task<ActionResult<Result>> CreateAsync(AddForfaitCommand addForfaitLambdaCommand)
        {
            return await Mediator.Send(addForfaitLambdaCommand);
        }


        //[HttpPut]
        //public async Task<ActionResult<Result>> UpdateAsync(ChangeForfaitCommand changeForfaitCommand)
        //{
        //    return await Mediator.Send(changeForfaitCommand);
        //}

        //[HttpDelete("User/{user_id}")]
        //public async Task<ActionResult<Result>> DeleteAsync(string user_id)
        //{
        //    return await Mediator.Send(new DeleteUserAndHisForfaitsCommand(user_id));
        //}

        //[Authorize("Resilience")]
        [HttpDelete("{user_id}")]
        public async Task<ActionResult<Result>> DeleteAsync(string user_id,[FromQuery]int idForfait)
        {
            return await Mediator.Send(new ResiliateForfaitCommand(user_id,idForfait));
        }

    }
}
