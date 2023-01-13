using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commentaries.Command.DeleteCommand
{
    public record DeleteCommentaryCommand(int id) : IRequest<Result>;
    public class DeleteCommentaryCommandHandler : IRequestHandler<DeleteCommentaryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCommentaryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteCommentaryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Commentaries
                .FindAsync(request.id)?? throw new NotFoundException(nameof(Commentaires), request.id);
            _context.Commentaries.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Commentary deleted with success");
            return Result.Failure("Action Failed : Commentary could not be deleted", new List<string>());
        }
    }
}
