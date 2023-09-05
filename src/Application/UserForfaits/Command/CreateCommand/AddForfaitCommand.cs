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

namespace Application.UserForfaits.Command.CreateCommand
{
    public record AddForfaitCommand(int idForfait) :IRequest<Result>;

    public class AddForfaitCommandHandler : IRequestHandler<AddForfaitCommand, Result>
    {

        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        public AddForfaitCommandHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(AddForfaitCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            await isPreviousForfait(request.idForfait,user_id);
            var newForfaitsUser = new Forfait_UserIntern()
            {
                IdForfait = request.idForfait,
                user_id = user_id,
            };
            _context.Forfait_Users.Add(newForfaitsUser);
   
            var result= await _context.SaveChangesAsync(cancellationToken);

            if (result>0)   return Result.Success("forfeit given with success"); 
            return Result.Failure("Action Failed : forfeit could not be given", new List<string>());
        }

       private async Task isPreviousForfait(int idForfait,string user_id)
        {
            var oldForfait = await retrieveOldForfait(user_id, idForfait);
            if (oldForfait == null) return;
            var oldForfaitUser = await _context.Forfait_Users
                                .Where(t => t.IdForfait == oldForfait.IdForfait && t.user_id == user_id)
                                .SingleOrDefaultAsync();
            _context.Forfait_Users.Remove(oldForfaitUser);
        }

        private async Task<ForfaitClient> retrieveOldForfait(string user_id, int idforfait)
        {
            List<ForfaitClient> forfaitClients = new List<ForfaitClient>();
            var forfait =await _context.Forfaits.FindAsync(idforfait);
           var userForfait= await _context.Forfait_Users.Where(t => t.user_id == user_id).ToListAsync();
            foreach (var user in userForfait)
            {
                forfaitClients.Add(await _context.Forfaits.FindAsync(user.IdForfait));
            }

            return forfaitClients
                .FirstOrDefault(t => t.RoleId == forfait.RoleId);
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
        //    var forfait=await getForfaitLambda(forfaits)??throw new InvalidOperationException("Pas de forfait lambda trouvé");
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
        //    foreach(var id in forfait_UserInterns) 
        //    { 
        //        var forfait=await _context.Forfaits.FindAsync(id.IdForfait);
        //        if (!forfait.IsForAuthor) return id;
        //    }
        //    return null;
        //}
    }


}
