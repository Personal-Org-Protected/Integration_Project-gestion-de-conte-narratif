using Application.Common.Models;
using Application.UserCases.UserLambda.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UseCaseUserController : ApiController
    {
        

        [HttpPost("creation-user")]
        public async Task<ActionResult<Result>> PostCreationUserAsync(UseCase_CreationUser useCase_Creation)//modified
        {
            return await Mediator.Send(useCase_Creation);
        }

        [HttpPost("achat-livre")]
        public async Task<ActionResult<Result>> PostAchatLivreAsync(UseCase_AcheterLivre useCase_AcheterLivre)//modified
        {
            return await Mediator.Send(useCase_AcheterLivre);
        }


    }
}
