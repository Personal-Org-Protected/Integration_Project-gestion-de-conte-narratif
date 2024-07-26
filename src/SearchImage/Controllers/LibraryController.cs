using Application.Common.Models;
using Application.Libraries.Command.CreateCommand;
using Application.Libraries.Command.UpdateCommand;
using Application.Libraries.Queries;
using Microsoft.AspNetCore.Authorization;
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
        //native
        [Authorize("UserAccess")]
        [HttpGet]
        public async Task<LibrariesDto> GetByOwner()//modified
        {
            return await Mediator.Send(new GetLibraryByOwnerQueries());
        }

   

        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateLibraryCommand createLibrary)//modified
        {
            return await Mediator.Send(createLibrary);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Put(string id, UpdateLibraryCommand updateLibrary)//modified
        {
            if (id != updateLibrary.libraryId)
            {
                return BadRequest();
            }
            return await Mediator.Send(updateLibrary);
        }

    }
}
