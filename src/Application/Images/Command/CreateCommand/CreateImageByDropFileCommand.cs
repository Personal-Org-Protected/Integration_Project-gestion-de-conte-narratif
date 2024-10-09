using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Command.CreateCommand
{
    public record CreateImageByDropFileCommand(IFormFile formFile, string descriptionImage, int IdTag) : IRequest<Result>;
 

    public class CreateImageByDropFileCommandHandler : IRequestHandler<CreateImageByDropFileCommand, Result>
    {

        private readonly IUser _user;
        private readonly IApplicationDbContext _context;

        public CreateImageByDropFileCommandHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Result> Handle(CreateImageByDropFileCommand request, CancellationToken cancellationToken)
        {
            var user_id = "testUser";//_user.getUserId();
            var tag = await getDefaultTag(request.IdTag);

            //bool isAlready = await ImageAlreadyExist(user_id, request.formFile.Name, cancellationToken);
            //if (isAlready) { return Result.Failure("Action Failed : image already exist", new List<string>()); }
            //var format = request.Uri[request.Uri.LastIndexOf('.')..];
            var entity = new Image()
            {
                NomImage = request.formFile.FileName,
                descriptionImage = request.descriptionImage,
                PathImage = "path not available",//CreateImageFile.PathImage(request.NomImage, CreateImageFile.GetFormat(format)),
                IdTag = tag,
                Uri = "no uri",
                DateCreation = DateTime.Now,
                user_id = user_id
            };
            await _context.Images.AddAsync(entity);
            await changeTagCount(tag);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 1)
            {
                // CreateImageFile.EnsureImageCreated(request.Uri,request.NomImage,CreateImageFile.GetFormat(format));
                return Result.Success("Image added with success");
            }
            return Result.Failure("Action Failed : image could not be added", new List<string>());
        }


        private async Task<int> getDefaultTag(int idTag)
        {
            if (idTag != 0) return idTag;

            var result = await _context.Tag
                     .Where(t => t.NameTag == "Default")
                     .SingleOrDefaultAsync() ?? throw new NotFoundException("no tag by default found"); idTag = result.IdTag;

            return idTag;
        }
        private async Task changeTagCount(int idTag)
        {
            var tag = await _context.Tag
                .FindAsync(idTag);
            tag.numberRef++;
            _context.Tag.Update(tag);
        }

        private async Task<bool> ImageAlreadyExist(string user_id, string uri, CancellationToken cancellationToken)
        {
            return !await _context.Images
                            .AnyAsync(t => t.Uri == uri, cancellationToken);
        }
    }
}
