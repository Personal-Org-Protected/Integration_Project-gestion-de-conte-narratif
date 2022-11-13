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

namespace Application.Users.Command.UpdateCommand
{
    public class ChoiceForfaitCommand : IRequest<Result>
    {
        public string user_id { get; set; }
        public int IdForfait { get; set; }
    }

    public class ChoiceForfaitCommandHandler : IRequestHandler<ChoiceForfaitCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuth0Client<UserUpdate> _client;

        public ChoiceForfaitCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }
        public async Task<Result> Handle(ChoiceForfaitCommand request, CancellationToken cancellationToken)
        {
            await _context.beginTransaction();
            //pick the idrole from the forfait
            var forfait = await _context.Forfaits.FindAsync(request.IdForfait);
            if (forfait == null) throw new NotFoundException();

            if (await userAlreadyHavethisForfeit(request.user_id,request.IdForfait))throw new InvalidOperationsException();
            if(await userAlreadyHaveForfeit(request.user_id)) return await ChangeForfeit(request.user_id,request.IdForfait,cancellationToken);
                //then look for the reference in the roles the auth0 id
            var authResult = await UserAuth(request,forfait);
            var identityResult = await UserIdentity(request, forfait, cancellationToken);
            var entityResult = await UserEntity(request, cancellationToken);

            if (await onCheckState(authResult, identityResult, entityResult)) { await _context.commitTransaction(); return Result.Success("role given with success"); }
            return Result.Failure("Action Failed : role could not be given", new List<string>());
        }
        private async Task<bool> onCheckState(bool auth, int identity, int entity)
        {
            if (!auth && identity != 1 && entity != 1)
            {
                await _context.rollbackTransaction();
                return false;
            }

            return true;
        }
        private async Task<bool> updateUserAuth(string user_id, string roleId)
        {
            var result = await _client.AddressingRole(user_id, roleId);
            return result.Succeeded;
        }
        private async Task<int> updateUserIdentity(User user, int idRole,CancellationToken cancellationToken)
        {
            var RolesUser = new Roles_Users()
            {
                idRole = idRole,
                user_id = user.user_id,
            };

            _context.Roles_Users.Add(RolesUser);
            return await _context.SaveChangesAsync(cancellationToken);
        }
        private async Task<int> updateUserEntity(int idforfait, UserEntity userEntity, CancellationToken cancellationToken)
        {
            var ForfaitsUser = new Forfait_UserIntern()
            {
                IdForfait = idforfait,
                IdUser = userEntity.IdUser,
            };
            _context.Forfait_Users.Add(ForfaitsUser);
           return await _context.SaveChangesAsync(cancellationToken);
        }



        private async Task<int> UserIdentity(ChoiceForfaitCommand request,ForfaitClient forfait, CancellationToken cancellationToken)
        {
            var Entity = await _context.Users
                .FirstOrDefaultAsync(t => t.user_id == request.user_id) ?? throw new NotFoundException();
            return await updateUserIdentity(Entity,forfait.RoleId, cancellationToken);
        }
        private async Task<int> UserEntity(ChoiceForfaitCommand request,CancellationToken cancellationToken)
        {
            var InternUser = await _context.userEntities
                .FirstOrDefaultAsync(t => t.user_id == request.user_id) ?? throw new NotFoundException();
            return await updateUserEntity(request.IdForfait, InternUser, cancellationToken);
        }
        private async Task<bool> UserAuth(ChoiceForfaitCommand request,ForfaitClient forfait)
        {
         
            var newRole = await _context.Roles.FindAsync(forfait.RoleId);
            if (newRole == null) throw new NotFoundException();

           return await updateUserAuth(request.user_id, newRole.AuthRoleId);

        }

        private async Task<bool> userAlreadyHaveForfeit(string user_id)
        {
            
            var user_role=await _context.Roles_Users
                .Where(t=>t.idRole==2 && t.user_id==user_id)
                .FirstOrDefaultAsync();
            if (user_role == null) return false;

            return true;
        }

        private async Task<bool> userAlreadyHavethisForfeit(string user_id, int forfeit)
        {
            var user = await _context.userEntities.FirstOrDefaultAsync(t => t.user_id == user_id);
            var user_role = await _context.Forfait_Users
                .Where(t => t.IdForfait == forfeit && t.IdUser==user.IdUser)
                .FirstOrDefaultAsync();
            if (user_role == null) return false;

            return true;
        }
        private async Task<Result> ChangeForfeit(string user_id,int NewForfait,CancellationToken cancellation)
        {
            var user=await _context.userEntities.FirstOrDefaultAsync(t=>t.user_id== user_id);

            _context.Forfait_Users.Add(new Forfait_UserIntern()
            {
                IdForfait=NewForfait,
                IdUser=user.IdUser,
            });

            _context.Forfait_Users.Remove(await GetForfait_UserIntern(user.IdUser));
            var resultTask=  await _context.SaveChangesAsync(cancellation);
            if (resultTask>0) { await _context.commitTransaction(); return Result.Success("change forfeit  with success"); }
            return Result.Failure("Action Failed : forfeit could not be changed", new List<string>());
        }

        private async Task<Forfait_UserIntern> GetForfait_UserIntern(string IdUser)
        {
            var forfait = await _context.Forfaits
                .Where(t => t.IsForAuthor)
                .SingleOrDefaultAsync();
            var oldUserforfait = await _context.Forfait_Users
                .Where(t => t.IdUser == IdUser && t.IdForfait!=4 && t.IdForfait != forfait.IdForfait)//souci null reference si le user n'a aps de forfait author l'obje sera null donc exception
                .FirstOrDefaultAsync();
            return oldUserforfait ?? throw new NotFoundException();
        }
    }
}
