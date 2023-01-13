using Application.Chapitres.Command.CreateCommand;
using Application.Chapitres.Command.DeleteCommand;
using Application.Chapitres.Command.UpdateCommand;
using Application.Chapitres.Queries;
using Application.Chapitres.Queries.Dto;
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
    public class ChapitreController : ApiController
    {
        //   [Authorize("ReadContent")]
        [HttpGet("{id}")]
       public async Task<ChapitresDto> GetById(int id)
        {
            return await Mediator.Send(new GetChapitresByIdQueries(id));
        }
        //[Authorize("AuthorAccess")]
        [HttpGet("StoryTell/{id}")]
        public async Task<PaginatedItems<ChapitresDto>> GetByIdStoryTelling( int id,[FromQuery] int pgNumber)
        {
            return await Mediator.Send(new GetChapitresByIdStoryTellingQueries(id,pgNumber));
        }

        //[Authorize("AuthorAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateChapitresCommand createChapitres)
        {
            return await Mediator.Send(createChapitres);
        }

        //[Authorize("AuthorAccess")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Put(int id,UpdateChapitreCommand updateChapitre)
        {
            if (id != updateChapitre.idChapitre)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateChapitre);
        }

        //[Authorize("AuthorAccess")]
        [HttpPut("order/{id}")]
        public async Task<ActionResult<Result>> Put2(int id, ChangeOrderCommand changeOrder)
        {
            if (id != changeOrder.idChapitre)
            {
                return BadRequest();
            }
            return await Mediator.Send(changeOrder);
        }

        //[Authorize("AuthorAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteChapitresCommand(id));
        }

        //[Authorize("AuthorAccess")]
        [HttpDelete("histoire/{id}")]
        public async Task<ActionResult<Result>> Delete2(int id)
        {
            return await Mediator.Send(new DeleteAllChaptersOfStoryCommand(id));
        }
    }
}
