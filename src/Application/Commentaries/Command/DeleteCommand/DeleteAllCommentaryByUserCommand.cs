using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
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
    public record DeleteAllCommentaryByUserCommand(string user_id):IRequest<Result>;

    public class DeleteCommentaryByUserCommandHandler : IRequestHandler<DeleteAllCommentaryByUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCommentaryByUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteAllCommentaryByUserCommand request, CancellationToken cancellationToken)
        {
            var Commentaries = await _context.Commentaries
                .Where(t=>t.user_id == request.user_id)
                .ToListAsync() ?? throw new NotFoundException();
            _context.Commentaries.RemoveRange(Commentaries);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Commentary deleted with success");
            return Result.Failure("Action Failed : Commentary could not be deleted", new List<string>());
        }
    }
}
