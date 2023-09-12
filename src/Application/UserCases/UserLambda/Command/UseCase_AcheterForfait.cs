using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Methods;
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

namespace Application.UserCases.UserLambda.Command
{
    public record UseCase_AcheterForfait(int idForfait):IRequest<Result>;

    public class UseCase_AcheterForfaitHandler : IRequestHandler<UseCase_AcheterForfait, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly int _roleAuthor = 3;
        private readonly int _roleFormerAuthor = 4;
        private readonly IAuth0Client<UserUpdate> _client;


        public UseCase_AcheterForfaitHandler(IApplicationDbContext context, IUser user, IAuth0Client<UserUpdate> client)
        {
            _context = context;
            _client = client;
            _user = user;
        }
 

        public async Task<Result> Handle(UseCase_AcheterForfait request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = _user.getUserId();
            var forfait=await _context.Forfaits.FindAsync(request.idForfait)?? throw new NotFoundException();
            try
            {
                await  _context.beginTransactionAsync();
                await process(forfait, user_id);
                result =  await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();
            }
            catch (Exception ex)
            {
               await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Forfait_Change", null);
            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result,"Forfait_Change",null);
        }

        private async Task process(ForfaitClient forfait,string user_id)
        {
            var hasRole = await hasFormerRole(user_id);
            await removeForfait(forfait.IdForfait, user_id);
            await addForfait(forfait.IdForfait, user_id);
            if (forfait.RoleId == _roleAuthor && hasRole)
            {
                await removeRole(user_id);
                await addRole(user_id);
                await authContext(user_id,hasRole);
            }
            else if (forfait.RoleId == _roleAuthor)
            {
                await addRole(user_id);
                await authContext(user_id,hasRole);
            }

        }

        private async Task addForfait(int idForfait,string user_id)
        {
            var newForfaitsUser = new Forfait_UserIntern()
            {
                IdForfait = idForfait,
                user_id = user_id,
            };
            await _context.Forfait_Users.AddAsync(newForfaitsUser);
        }

        private async Task removeForfait(int idForfait, string user_id)
        {
            var oldForfait = await retrieveOldForfait(user_id, idForfait);
            if (oldForfait == null) return;
            var oldForfaitUser = await _context.Forfait_Users
                                .Where(t => t.IdForfait == oldForfait.IdForfait && t.user_id == user_id)
                                .SingleOrDefaultAsync()?? throw new NotFoundException();
            _context.Forfait_Users.Remove(oldForfaitUser);
        }


        private async Task<ForfaitClient> retrieveOldForfait(string user_id, int idforfait)
        {
            List<ForfaitClient> forfaitClients = new List<ForfaitClient>();
            var forfait = await _context.Forfaits.FindAsync(idforfait);
            var userForfait = await _context.Forfait_Users.Where(t => t.user_id == user_id).ToListAsync();
            foreach (var user in userForfait)
            {
                var pack = await _context.Forfaits.FindAsync(user.IdForfait);
                forfaitClients.Add(pack);
            }

            return forfaitClients
                .FirstOrDefault(t => t.RoleId == forfait.RoleId);
        }


        private async Task addRole(string user_id)
        {
            var role_user = new Roles_Users()
            {
                idRole = _roleAuthor,
                user_id = user_id
            };
            await _context.Roles_Users.AddAsync(role_user);
        }

        private async Task removeRole(string user_id)
        {
            var role_user = await _context.Roles_Users
                .Where(t => t.user_id == user_id && t.idRole == _roleFormerAuthor)
                .SingleOrDefaultAsync();
            _context.Roles_Users.Remove(role_user);
        }
        private async Task<bool> hasFormerRole(string user_id)
        {
            var role = await _context.Roles_Users.SingleOrDefaultAsync(t => t.idRole == _roleFormerAuthor && t.user_id==user_id);
            return role != null;
        }


        private async Task authContext(string user_id, bool hasFormer)
        {
            var roleAdd = await _context.Roles.FindAsync(_roleAuthor);
            if (hasFormer)
            {
                var roleRemove = await _context.Roles.FindAsync(_roleFormerAuthor);
                await removeAuth(user_id, roleRemove.AuthRoleId);
                await addAuth(user_id, roleAdd.AuthRoleId);
            }
            else
                await addAuth(user_id, roleAdd.AuthRoleId);

        }
        private async Task removeAuth(string user_id, string roleid)
        {

            await _client.DeleteRoleFromUser(user_id, roleid);
        }

        private async Task addAuth(string user_id, string roleid)
        {
            await _client.AddressingRole(user_id, roleid);
        }

    }
}
