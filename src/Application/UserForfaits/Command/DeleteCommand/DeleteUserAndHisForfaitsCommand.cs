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

namespace Application.UserForfaits.Command.DeleteCommand
{
    public record DeleteUserAndHisForfaitsCommand(string user_id):IRequest<Result>;

    public class DeleteUserAndHisForfaitsCommandHandler : IRequestHandler<DeleteUserAndHisForfaitsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly int _ForfaitId = 1;

        public DeleteUserAndHisForfaitsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteUserAndHisForfaitsCommand request, CancellationToken cancellationToken)
        {

            var result = await UpdateUserForfait(request.user_id,cancellationToken);
            if (result > 0) return Result.Success("all the user forfait have been deleted");

            return Result.Failure("user forfait could not be deleted", new List<string>() { "maybe not found" });
        }
        private async Task<int> UpdateUserForfait(string user_id, CancellationToken cancellationToken)
        {
            var forfaitUser = await _context.Forfait_Users
                .Where(t => t.user_id == user_id && t.IdForfait==_ForfaitId)
                .ToListAsync();
            _context.Forfait_Users.RemoveRange(forfaitUser);
          return   await _context.SaveChangesAsync(cancellationToken);
        }
    }


}
