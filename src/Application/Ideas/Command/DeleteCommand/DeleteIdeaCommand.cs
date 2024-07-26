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

namespace Application.Ideas.Command.DeleteCommand
{
    public record DeleteIdeaCommand(int id) : IRequest<Result>;

    public class DeleteIdeaCommandHandler : IRequestHandler<DeleteIdeaCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public DeleteIdeaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async  Task<Result> Handle(DeleteIdeaCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.Ideas
           .Where(l => l.IdIdee == request.id)
           .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Images), request.id);
            }
            _context.Ideas.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0)
            {
                return Result.Success("Idea deleted with success");
            }
            return Result.Failure("Action Failed : Idea could not be deleted", new List<string>());
        }
    }


}
