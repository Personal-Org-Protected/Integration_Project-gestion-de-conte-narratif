using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commentaries.Command.UpdateCommand
{
    public record LikeCommentaryCommand(int id) : IRequest<Result>;


    public class LikeCommentaryCommandHandler : IRequestHandler<LikeCommentaryCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public LikeCommentaryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(LikeCommentaryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context
                        .Commentaries
                        .FindAsync(request.id) ?? throw new NotFoundException(nameof(Commentaires), request.id); ;

            entity.like++;

            _context.Commentaries.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Commentary signaled with success");
            return Result.Failure("Action Failed : Commentary could not be signaled", new List<string>());

        }
    }
}
