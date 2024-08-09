using Application.Basket_items.Command.CreateCommand;
using Application.Basket_items.Command.DeleteCommand;
using Application.Basket_items.Queries;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{

    [Authorize("BasketAccess")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BasketItemController : ApiController
    {


        [HttpGet("count")]
        public async Task<int> Get()
        {
            return await Mediator.Send(new GetNumberItemQueries());
        }

        [HttpGet("{id}")]
        public async Task<PaginatedItems<BasketItemsDto>> GetById(string id,[FromQuery]int pgNumber)
        {
            return await Mediator.Send(new GetItemsFromBasketQueries(id,pgNumber));
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateItemsBasketCommand createItemsBasket)
        {
            return await Mediator.Send(createItemsBasket);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete([FromQuery]int storyTell, string id)
        {
            return await Mediator.Send(new DeleteItemBasketCommand(storyTell,id));
        }

        [HttpDelete("empty/{id}")]
        public async Task<ActionResult<Result>> Delete(string id)
        {
            return await Mediator.Send(new EmptyBucketCommand(id));
        }
    }
}
