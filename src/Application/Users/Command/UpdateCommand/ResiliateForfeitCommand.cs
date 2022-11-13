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

namespace Application.Users.Command.UpdateCommand
{
    public class ResiliateForfeitCommand :IRequest<Result>
    {
        public string user_id { get; set; }
        public int idForfait { get; set; }
    }

    public class ResiliateForfeitCommandHandler : IRequestHandler<ResiliateForfeitCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuth0Client<UserUpdate> _client;

        public ResiliateForfeitCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }
        public async Task<Result> Handle(ResiliateForfeitCommand request, CancellationToken cancellationToken)
        {
          await _context.beginTransaction();
            var authResult = await resiliateAuth(request);
            var identityResult=await resiliateIdentity(request,cancellationToken);
            var entityResult=await resiliateEntity(request,cancellationToken);

            if (authResult.Succeeded && identityResult.Succeeded && entityResult.Succeeded) { await _context.commitTransaction(); return Result.Success("Resiliation succeded"); }
            else { await _context.rollbackTransaction(); 
                var errors=new List<string>()
                {
                    authResult.Msg,
                    identityResult.Msg,
                    entityResult.Msg,
                };
                return Result.Failure("Resiliation Failed", errors); }
        }


        private async Task<Result> resiliateEntity(ResiliateForfeitCommand request,CancellationToken cancellationToken)
        {
            var userEntity = await _context.userEntities.FirstOrDefaultAsync(t => t.user_id == request.user_id);
            var usersForfait = await _context.Forfait_Users
               .Where(t => t.IdUser == userEntity.IdUser && t.IdForfait==request.idForfait)
               .SingleOrDefaultAsync() ?? throw new NotFoundException();

            _context.Forfait_Users.Remove(usersForfait);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Forfeit resiliated with success");
            return Result.Failure("Action Failed : Forfeit could not be resiliated", new List<string>());
        }

        private async Task<Result> resiliateIdentity(ResiliateForfeitCommand request, CancellationToken cancellationToken)
        {
            var forfait = await _context.Forfaits
                .FindAsync(request.idForfait);
            var userRole = await _context.Roles_Users
                .Where(t => t.user_id == request.user_id && t.idRole==forfait.RoleId)
                .SingleOrDefaultAsync();

            _context.Roles_Users.Remove(userRole);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("role resiliated with success");
            return Result.Failure("Action Failed : role could not be resiliated", new List<string>());
        }

        private async Task<Result> resiliateAuth(ResiliateForfeitCommand request)
        {
            var forfait = await _context.Forfaits.FindAsync(request.idForfait);
            var role=await _context.Roles.FindAsync(forfait.RoleId);

            return await _client.DeleteRoleFromUser(request.user_id, role.AuthRoleId);

        }
    }


}
