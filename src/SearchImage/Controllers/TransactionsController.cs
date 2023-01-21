using Application.Common.Models;
using Application.Transactions.Command.CreateCommand;
using Application.Transactions.Command.DeleteCommand;
using Application.Transactions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [Authorize("UserAccess")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TransactionsController  : ApiController
    {
        [HttpGet]
        public async Task<PaginatedItems<TransactionsDto>> GetByUser([FromQuery]GetTransactionQueries getTransaction)
        {
            return await Mediator.Send(getTransaction);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> PostAsync(CreateTransactionsCommand createTransactions)
        {
            return await Mediator.Send(createTransactions);
        }
        [HttpDelete("user/{user_id}")]
        public async Task<ActionResult<Result>> DeleteAsync( string user_id)
        {
            return await Mediator.Send(new DeleteAllUserTransactionCommand(user_id));
        }


    }
}
