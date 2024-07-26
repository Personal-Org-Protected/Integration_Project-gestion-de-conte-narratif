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

namespace Application.Ideas.Command.DeleteCommand
{
    public record DeleteAllIdeaCommand(int storyTell):IRequest<Result>;

    public class DeleteAllIdeaCommandHandler : IRequestHandler<DeleteAllIdeaCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAllIdeaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteAllIdeaCommand request, CancellationToken cancellationToken)
        {
            var ideas = await _context.Ideas
                .Where(t => t.IdStoryTelling==request.storyTell)
                .ToListAsync() ?? throw new NotFoundException();
            _context.Ideas.RemoveRange(ideas);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Commentary deleted with success");
            return Result.Failure("Action Failed : Commentary could not be deleted", new List<string>());
        }
    }
}
