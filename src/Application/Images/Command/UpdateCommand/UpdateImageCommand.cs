using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Command.UpdateCommand
{
    public class UpdateImageCommand : IRequest<Result>
    {
        public int IdImage { get; set; }
        public string NomImage { get; set; }
        public string descriptionImage { get; set; }
        public int IdTag { get; set; }
    }
    public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
         {
            var tag = await getDefaultTag(request.IdTag);
            var entity = await _context.Images
          .FindAsync(new object[] { request.IdImage }, cancellationToken)?? throw new NotFoundException(nameof(Image), request.IdImage); 

            var format=entity.PathImage[entity.PathImage.LastIndexOf('.')..];
            var OldPath = entity.PathImage;
            var NewPath = entity.PathImage.Replace($"{entity.NomImage}",$"{request.NomImage+format}");

            entity.NomImage=request.NomImage;
            entity.descriptionImage = request.descriptionImage;
            entity.IdTag = tag;
            entity.DateModif = DateTime.Now;
            entity.PathImage = "path not available";//NewPath;
            _context.Images.Update(entity);
            await changeTagCount(tag);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) 
            {
                //File.Move(OldPath, NewPath);
                return Result.Success("Image modified with success"); }
            return Result.Failure("Action Failed : image could not be modified", new List<string>());
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
    }
}
