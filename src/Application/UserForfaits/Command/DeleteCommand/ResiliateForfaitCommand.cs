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
    public record ResiliateForfaitCommand(int idForfait):IRequest<Result>;


    public class ResiliateForfaitCommandHandler : IRequestHandler<ResiliateForfaitCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public ResiliateForfaitCommandHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(ResiliateForfaitCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();

            var result = await UpdateUserForfait(user_id,request.idForfait, cancellationToken);
            if (result > 0) return Result.Success("all the user forfait have been deleted");

            return Result.Failure("user forfait could not be deleted", new List<string>() { "maybe user does not have this package" });
        }
        private async Task<int> UpdateUserForfait(string user_id, int idForfait,CancellationToken cancellationToken)
        {
            var forfaitUser = await _context.Forfait_Users
                .Where(t => t.user_id == user_id && t.IdForfait== idForfait)
                .SingleOrDefaultAsync();
            _context.Forfait_Users.Remove(forfaitUser);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
