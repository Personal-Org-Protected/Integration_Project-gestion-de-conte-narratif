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

namespace Application.Stories.Command.DeleteCommand
{
    public record DeleteStoriesCommand(int id):IRequest<Result>;

    public class DeleteStoriesCommandHandler : IRequestHandler<DeleteStoriesCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStoriesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteStoriesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Stories
            .Where(l => l.IdStory == request.id)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Story), request.id);
            }

            _context.Stories.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Story deleted with success");
            return Result.Failure("Action Failed : Story could not be deleted", new List<string>());
        }
    }

}
