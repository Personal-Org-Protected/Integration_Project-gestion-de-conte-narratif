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

namespace Application.StoryBox.Command.CreateCommand
{
    public record CreateStoryBoxAfterTransactionCommand(string user_id,int storyTell) : IRequest<Result>;


    public class CreateStoryBoxAfterTransactionCommandHandler : IRequestHandler<CreateStoryBoxAfterTransactionCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateStoryBoxAfterTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateStoryBoxAfterTransactionCommand request, CancellationToken cancellationToken)
        {

            var library = await _context.Libraries
           .Where(t => t.user_id == request.user_id)
           .FirstOrDefaultAsync() ?? throw new NotFoundException();

            var entity = new StoryTellBox()
            {
                IdStoryTell = request.storyTell,
                lastPageChecked = 0,
                IdLibrary = library.IdLibrary
            };
            _context.storyTellBoxes.Add(entity);
            var resulTask = await _context.SaveChangesAsync(cancellationToken);
            if (resulTask > 0) { return Result.Success("StoryBox added with success", entity.IdBox); }
            return Result.Failure("Action Failed : StoryBox could not be added", new List<string>());
        }
    } 

}
