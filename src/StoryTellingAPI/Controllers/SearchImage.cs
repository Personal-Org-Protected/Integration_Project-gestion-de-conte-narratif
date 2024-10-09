using Application.Common.Models;
using Application.Images.Command.CreateCommand;
using Application.Images.Command.DeleteCommand;
using Application.Images.Command.UpdateCommand;
using Application.Images.Queries;
using Application.Images.Queries.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{

    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SearchImage : ApiController
    {

        [Authorize("AuthorAccess")]
        [HttpGet]
        public async Task<ActionResult<PaginatedItems<ImageDto>>> Get([FromQuery] GetImageByOwnerQueries imageByOwnerQueries)//modified
        {
            return await Mediator.Send(imageByOwnerQueries);
        }



        //native
        [Authorize("ReadContent")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> GetById(int id)
        {
            return await Mediator.Send(new GetImagesByIdQueries(id));
        }

     

        [Authorize("AuthorAccess")]
        [HttpGet("alreadyHasChapter/{id}")]
        public async Task<AlreadyInChapter> GetById2(int id)
        {
            return await Mediator.Send(new ImageAlreadyInChapter(id));
        }

        [Authorize("AuthorAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateImageCommand createImageCommand)//modified
        {
           return  await Mediator.Send(createImageCommand);
        }

        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile, [FromBody] string description, [FromQuery] int idTag )
        //{
        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        return BadRequest("No file was uploaded.");
        //    }
        //    await Mediator.Send(new CreateImageByDropFileCommand(imageFile,description,idTag));
        //    // Return success message
        //    return Ok(new { message = "File uploaded successfully!" });
        //}


        [Authorize("AuthorAccess")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id, UpdateImageCommand updateImagecommand)
        {
            if (id != updateImagecommand.IdImage)
            {
                return BadRequest();
            }

         return await Mediator.Send(updateImagecommand);

        }


        [Authorize("AuthorAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteImageCommand(id));
        }

    }
}
