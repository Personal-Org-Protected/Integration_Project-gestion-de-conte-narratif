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
    public record ResiliateForfaitCommand(string user_id,int idForfait):IRequest<Result>;


    public class ResiliateForfaitCommandHandler : IRequestHandler<ResiliateForfaitCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public ResiliateForfaitCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(ResiliateForfaitCommand request, CancellationToken cancellationToken)
        {


            var result = await UpdateUserForfait(request.user_id,request.idForfait, cancellationToken);
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
