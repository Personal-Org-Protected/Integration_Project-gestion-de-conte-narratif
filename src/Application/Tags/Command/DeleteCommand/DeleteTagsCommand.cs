using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Command.DeleteCommand
{
    public record DeleteTagsCommand(int id):IRequest<Result>;

    public class DeleteTagsCommandHandler : IRequestHandler<DeleteTagsCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTagsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteTagsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tag
            .Where(l => l.IdTag == request.id)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Images), request.id);
            }

            _context.Tag.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Tag deleted with success");
            return Result.Failure("Action Failed : Tag could not be deleted", new List<string>());
        }
    }

}
