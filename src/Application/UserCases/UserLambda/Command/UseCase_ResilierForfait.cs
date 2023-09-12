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
    public record UseCase_ResilierForfait(int idForfait) : IRequest<Result>;

    public class UseCase_ResilierForfaitHandler : IRequestHandler<UseCase_ResilierForfait, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly int _defaultForfait = 1;
        private readonly int _defaultRole = 2;
        private readonly int _roleAuthor = 3;
        private readonly int _roleFormerAuthor = 4;
        private readonly IAuth0Client<UserUpdate> _client;


        public UseCase_ResilierForfaitHandler(IApplicationDbContext context, IUser user, IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
            _user = user;
        }


        public async Task<Result> Handle(UseCase_ResilierForfait request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = _user.getUserId();
            var forfait = await _context.Forfaits.FindAsync(request.idForfait) ?? throw new NotFoundException();
            try
            {
                await _context.beginTransactionAsync();

                await process(forfait, user_id);
                
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Forfait_Resilience", null);

            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Forfait_Resilience", null);
        }


        private async Task process(ForfaitClient forfait,string user_id)
        {
            if (forfait.IdForfait != _defaultForfait)
            {
                await removeForfait(forfait.IdForfait, user_id);
            }

            if (forfait.RoleId == _defaultRole && forfait.IdForfait != _defaultForfait)
            {
                await addForfait(_defaultForfait, user_id);
            }

            if (forfait.RoleId == _roleAuthor)
            {
                await removeRole(user_id);
                await addRole(user_id);
                await authContext(user_id);
            }

        }
        private async Task addForfait(int idForfait, string user_id)
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
            var oldForfaitUser = await _context.Forfait_Users
                                .Where(t => t.IdForfait == idForfait && t.user_id == user_id)
                                .SingleOrDefaultAsync();
            _context.Forfait_Users.Remove(oldForfaitUser);
        }

        private async Task addRole(string user_id)
        {
            var role_user = new Roles_Users()
            {
                idRole = _roleFormerAuthor,
                user_id = user_id
            };
            await _context.Roles_Users.AddAsync(role_user);
        }

        private async Task removeRole(string user_id)
        {
            var role_user = await _context.Roles_Users
                .Where(t => t.user_id == user_id && t.idRole == _roleAuthor)
                .SingleOrDefaultAsync();
            _context.Roles_Users.Remove(role_user);
        }


        private async Task authContext(string user_id)
        {
            var roleRemove = await _context.Roles.FindAsync(_roleAuthor);
            var roleAdd = await _context.Roles.FindAsync(_roleFormerAuthor);
            await removeAuth(user_id, roleRemove.AuthRoleId);
            await addAuth(user_id, roleAdd.AuthRoleId);
        }
        private async Task removeAuth(string user_id,string roleid)
        {
            
            await _client.DeleteRoleFromUser(user_id, roleid);
        }

        private async Task addAuth(string user_id, string roleid)
        {
            await _client.AddressingRole(user_id, roleid);
        }



    }
}
