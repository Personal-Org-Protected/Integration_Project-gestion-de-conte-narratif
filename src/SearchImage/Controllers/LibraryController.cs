using Application.Common.Models;
using Application.Libraries.Command.CreateCommand;
using Application.Libraries.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LibraryController : ApiController
    {
        [HttpGet]
        public async Task<LibrariesDto> GetByOwner([FromQuery] GetLibraryByOwnerQueries getLibraryByOwner )
        {
            return await Mediator.Send(getLibraryByOwner);
        }

        [HttpPost]
        public async Task<Result> Post(CreateLibraryCommand createLibrary)
        {
            return await Mediator.Send(createLibrary);
        }

    }
}
