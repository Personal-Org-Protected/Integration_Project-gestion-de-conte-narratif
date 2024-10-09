using Application.Baskets.Command.CreateCommand;
using Application.Baskets.Queries;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BasketController : ApiController
    {
        [Authorize("ReadContent")]
        [HttpGet]
        public async Task<BasketDto> Get()
        {
            return await Mediator.Send(new GetBasketByUserQueries());
        }



        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateBasketCommand createBasket)
        {
            return await Mediator.Send(createBasket);
        }

    }
}
