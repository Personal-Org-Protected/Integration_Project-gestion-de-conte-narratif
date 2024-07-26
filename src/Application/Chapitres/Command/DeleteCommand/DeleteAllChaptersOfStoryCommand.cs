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

namespace Application.Chapitres.Command.DeleteCommand
{
    public record DeleteAllChaptersOfStoryCommand(int storyTell):IRequest<Result>;

    public class DeleteCommentaryByUserCommandHandler : IRequestHandler<DeleteAllChaptersOfStoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCommentaryByUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteAllChaptersOfStoryCommand request, CancellationToken cancellationToken)
        {
            var Chapitres = await _context.Chapitres
                .Where(t => t.IdStoryTelling == request.storyTell)
                .ToListAsync() ?? throw new NotFoundException();
            _context.Chapitres.RemoveRange(Chapitres);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Commentary deleted with success");
            return Result.Failure("Action Failed : Commentary could not be deleted", new List<string>());
        }
    }
}
