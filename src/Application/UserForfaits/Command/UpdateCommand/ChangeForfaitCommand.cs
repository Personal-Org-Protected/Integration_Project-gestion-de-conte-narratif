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

namespace Application.UserForfaits.Command.UpdateCommand
{
    public record  ChangeForfaitCommand(string user_id,int newForfait,int oldForfait) : IRequest<Result>;

    public class ChangeForfaitCommandHandler : IRequestHandler<ChangeForfaitCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public ChangeForfaitCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(ChangeForfaitCommand request, CancellationToken cancellationToken)
        {

            _context.Forfait_Users.Add(new Forfait_UserIntern()
            {
                IdForfait = request.newForfait,
                user_id = request.user_id,
            });

            var forfait = await  _context.Forfait_Users
                .Where(t => t.IdForfait == request.oldForfait && t.user_id==request.user_id)
                .SingleOrDefaultAsync() ?? throw new  NotFoundException(); 

            _context.Forfait_Users.Remove(forfait);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);
            if (resultTask > 1) { return Result.Success("change forfeit  with success"); }
            return Result.Failure("Action Failed : forfeit could not be changed", new List<string>());
        }
    }

    //private async Task<Result> changeForfeit(string user_id, int NewForfait, CancellationToken cancellation)
    //{
    //    var user = await _context.userEntities.FirstOrDefaultAsync(t => t.user_id == user_id);

    //    _context.Forfait_Users.Add(new Forfait_UserIntern()
    //    {
    //        IdForfait = NewForfait,
    //        IdUser = user.IdUser,
    //    });

    //    var forfaits = await GetForfait_UserIntern(user_id);
    //    var forfait = await getForfaitLambda(forfaits) ?? throw new InvalidOperationException("Pas de forfait lambda trouvé");
    //    _context.Forfait_Users.Remove(forfait);
    //    var resultTask = await _context.SaveChangesAsync(cancellation);
    //    if (resultTask > 0) { return Result.Success("change forfeit  with success"); }
    //    return Result.Failure("Action Failed : forfeit could not be changed", new List<string>());
    //}

    //private async Task<List<Forfait_UserIntern>> GetForfait_UserIntern(string IdUser)
    //{
    //    return await _context.Forfait_Users
    //        .Where(t => t.IdUser == IdUser && t.IdForfait > 4)
    //        .ToListAsync();
    //}

    //private async Task<Forfait_UserIntern> getForfaitLambda(List<Forfait_UserIntern> forfait_UserInterns)
    //{
    //    foreach (var id in forfait_UserInterns)
    //    {
    //        var forfait = await _context.Forfaits.FindAsync(id.IdForfait);
    //        if (!forfait.IsForAuthor) return id;
    //    }
    //    return null;
    //}
}
