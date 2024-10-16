﻿using Application.Common.Models;
using Application.Forfaits.Command.CreateCommand;
using Application.Forfaits.Command.DeleteCommand;
using Application.Forfaits.Command.UpdateCommand;
using Application.Forfaits.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
   
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ForfaitController :ApiController
    {

        [HttpGet]
        public async Task<PaginatedItems<ForfaitDto>> Get([FromQuery] GetForfaitQueries getForfait)
        {
            return await Mediator.Send(getForfait);
        }



        [Authorize("ForfaitAccess")]
        [HttpGet("{id}")]
        public async Task<ForfaitDto> Get(int id)
        {
            return await Mediator.Send(new GetForfaitByIdQueries(id));
        }



        [Authorize("ForfaitAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateForfaitCommand createForfait)
        {
            return await Mediator.Send(createForfait);
        }


        [Authorize("ForfaitAccess")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id,UpdateForfaitCommand updateForfait)
        {

            if (id != updateForfait.IdForfait)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateForfait);
        }


        [Authorize("ForfaitAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteUserLambdaForfaitCommand(id));
        }
    }
}
