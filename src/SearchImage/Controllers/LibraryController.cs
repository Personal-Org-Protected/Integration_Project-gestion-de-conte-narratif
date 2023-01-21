using Application.Common.Models;
using Application.Libraries.Command.CreateCommand;
using Application.Libraries.Queries;
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
    public class LibraryController : ApiController
    {
        [HttpGet("{user_id}")]
        public async Task<LibrariesDto> GetByOwner(string user_id)
        {
            return await Mediator.Send(new GetLibraryByOwnerQueries(user_id));
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateLibraryCommand createLibrary)
        {
            return await Mediator.Send(createLibrary);
        }

    }
}
