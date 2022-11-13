using Application.Chapitres.Command.CreateCommand;
using Application.Chapitres.Command.DeleteCommand;
using Application.Chapitres.Queries;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{   [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ChapitreController : ApiController
    {
       // [Authorize("ReadAccess")]
        [HttpGet("{id}")]
       public async Task<ChapitresDto> GetById(int id)
        {
            return await Mediator.Send(new GetChapitresByIdQueries(id));
        }
        //[Authorize("ReadAccess")]
        [HttpGet]
        public async Task<PaginatedItems<ChapitresDto>> GetByIdStoryTelling([FromQuery]GetChapitresByIdStoryTellingQueries chapitreQueries)
        {
            return await Mediator.Send(chapitreQueries);
        }

       // [Authorize("WriteAccess")]
        [HttpPost]
        public async Task<Result> Post(CreateChapitresCommand createChapitres)
        {
            return await Mediator.Send(createChapitres);
        }

        //[Authorize("DeleteAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteChapitresCommand(id));
        }
    }
}
