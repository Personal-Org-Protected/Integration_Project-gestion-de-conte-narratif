using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Command.UpdateCommand
{
    public class UpdateStoryTellingCommand :IRequest<Result>
    {
        public int Id { get; set; }
        public string NameStory { get; set; }
        public string url { get; set; }
        public double price { get; set; }//new 
        public string synopsis { get; set; }//new
        public int idTag { get; set; }//new
    }

    public class UpdateStoryTellingCommandHandler : IRequestHandler<UpdateStoryTellingCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStoryTellingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateStoryTellingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.StoryTellings
           .FindAsync(new object[] { request.Id }, cancellationToken)?? throw new NotFoundException(nameof(Transaction), request.Id); 

            entity.NameStory = request.NameStory;
            entity.url = request.url;
            entity.Sypnopsis= request.synopsis;
            entity.idTag = await getDefaultTag(request.idTag);

            _context.StoryTellings.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("StoryTellings updated with success");
            return Result.Failure("Action Failed : StoryTellings could not be updated", new List<string>());
        }

        private async Task<int> getDefaultTag(int idTag)
        {
            if (idTag == 0)
            {
                var result = await _context.Tag
                                   .Where(t => t.NameTag == "Default")
                                   .SingleOrDefaultAsync() ?? throw new NotFoundException("no tag by default found"); idTag = result.IdTag;
            }
            return idTag;
        }
    }
}
