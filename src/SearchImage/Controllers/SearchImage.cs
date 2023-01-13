using Application.Common.Models;
using Application.Images.Command.CreateCommand;
using Application.Images.Command.DeleteCommand;
using Application.Images.Command.UpdateCommand;
using Application.Images.Queries;
using Application.Images.Queries.Dto;
using Application.ImagesClient.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchImage.Controllers
{

    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SearchImage : ApiController
    {

        //[Authorize("ReadContent")]
        [HttpGet]
        public async Task<ActionResult<PaginatedItems<ImageDto>>> Get([FromQuery] GetImageByOwnerQueries imageByOwnerQueries)
        {
            return await Mediator.Send(imageByOwnerQueries);
        }



        /// <summary>
        /// appelle l'api externe pour selectionner une serie d'image selon certains criteres
        /// </summary>
        /// <param name="imagesQueries"></param>
        /// <returns></returns>
        [HttpGet("ImageClient")]
        public async Task<ImageClientVM> GetImageClient([FromQuery] ImagesClientQueries imagesQueries)
        {
            return await Mediator.Send(imagesQueries);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> GetById(int id)
        {
            return await Mediator.Send(new GetImagesByIdQueries(id));
        }

       // [Authorize("AuthorAccess")]
        [HttpGet("alreadyHasChapter/{id}")]
        public async Task<AlreadyInChapter> GetById2(int id)
        {
            return await Mediator.Send(new ImageAlreadyInChapter(id));
        }

      //  [Authorize("AuthorAccess")]
        [HttpPost]
        public async Task<ActionResult<Result>> Post(CreateImageCommand createImageCommand)
        {
           return  await Mediator.Send(createImageCommand);
        }


       // [Authorize("AuthorAccess")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update(int id, UpdateImageCommand updateImagecommand)
        {
            if (id != updateImagecommand.IdImage)
            {
                return BadRequest();
            }

         return await Mediator.Send(updateImagecommand);

        }


       // [Authorize("AuthorAccess")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteImageCommand(id));
        }

    }
}
