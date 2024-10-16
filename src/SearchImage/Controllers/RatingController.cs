﻿using Application.Common.Models;
using Application.Rating_Info.Command.CreateCommand;
using Application.Rating_Info.Command.UpdateCommand;
using Application.Rating_Info.Queries;
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
    public class RatingController : ApiController 
    {
        [Authorize("ReadContent")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AlreadyRated>> Get(int id)//modified
        {
            return await Mediator.Send(new GetRatingInfoQueries(id));
        }


        [Authorize("UserAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateRatingInfoCommand createRatingInfoCommand)//modified
        {
            return await Mediator.Send(createRatingInfoCommand);
        }


        [Authorize("UserAccess")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id, UpdateRatingInfoCommand updateRatingInfoCommand)
        {
            if (id != updateRatingInfoCommand.id)
            {
                return BadRequest();
            }

            return await Mediator.Send(updateRatingInfoCommand);

        }

    }
}
