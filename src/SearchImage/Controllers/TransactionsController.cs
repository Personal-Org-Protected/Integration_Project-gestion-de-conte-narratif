using Application.Common.Models;
using Application.Transactions.Command.CreateCommand;
using Application.Transactions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
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
        public async Task<Result> Post(CreateTransactionsCommand createTransactions)
        {
            return await Mediator.Send(createTransactions);
        }


    }
}
