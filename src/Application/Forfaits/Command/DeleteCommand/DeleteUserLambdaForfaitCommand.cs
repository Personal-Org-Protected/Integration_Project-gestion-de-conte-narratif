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

namespace Application.Forfaits.Command.DeleteCommand
{
    public record DeleteUserLambdaForfaitCommand(int id) : IRequest<Result>;

    public class DeleteForfaitCommandHandler : IRequestHandler<DeleteUserLambdaForfaitCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuth0Client<UserUpdate> _client;

        public DeleteForfaitCommandHandler(IApplicationDbContext context,IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }
        public async Task<Result> Handle(DeleteUserLambdaForfaitCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Forfaits
                                  .Where(l => l.IdForfait == request.id)
                                  .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ForfaitClient), request.id);
            }
            var resultUserIntern=await resilateForfeit(entity,cancellationToken);
            var resultUser = await RoleChange(entity, cancellationToken);
            var resultAuth = await authDeleteRole(entity);

            if (!resultUserIntern.Succeeded || !resultUser.Succeeded) return Result.Failure("operation delete failed",new List<string>() { resultUserIntern.Msg,resultUser.Msg,resultAuth.Msg});

            _context.Forfaits.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Forfeit deleted with success");
            return Result.Failure("Action Failed : Forfeit could not be deleted", new List<string>());
        }


        private async Task<Result> resilateForfeit(ForfaitClient forfait,CancellationToken cancellationToken)
        {
            if (verifyNotAuthor(forfait)) throw new InvalidOperationException("this forfeit is for author it can not be deleted like that");
            var usersForfait = await _context.Forfait_Users
                .Where(t => t.IdForfait == forfait.IdForfait)
                .ToListAsync();
            foreach(var user in usersForfait)
            {
                _context.Forfait_Users.Remove(user);
            }
           var resultTask = await  _context.SaveChangesAsync(cancellationToken);
            if (resultTask > 0) return Result.Success("Forfeit resiliated with success");
            return Result.Failure("Action Failed : Forfeit could not be resiliated", new List<string>());
        }

        private  bool verifyNotAuthor(ForfaitClient forfait)
        {
            if(forfait.IsForAuthor)return true;
            else return false;
        }

        private async Task<Result> RoleChange(ForfaitClient forfait, CancellationToken cancellationToken)
        {
            var usersForfait = await _context.Forfait_Users
                .Where(t => t.IdForfait == forfait.IdForfait)
                .ToListAsync();


            var ids = await getListIdUser(usersForfait);
            var userRoles=await _context.Roles_Users
                .Where(t => t.idRole == 2 && ids.Contains(t.user_id))
                .ToListAsync();

            _context.Roles_Users.RemoveRange(userRoles);
            var resultTask=await _context.SaveChangesAsync(cancellationToken);
            if (resultTask > 0) return Result.Success("user role changed with success");
            return Result.Failure("Action Failed : user role could not be changed ", new List<string>());

        }

        private async Task<List<UserEntity>> GetUsersEntity(List<Forfait_UserIntern> usersForfait)
        {
            var userList = new List<string>();
            foreach (var user in usersForfait)
            {
                userList.Add(user.IdUser);
            }
            var userIntern = await _context.userEntities
                .Where(t => userList.Contains(t.IdUser))
                .ToListAsync();
            return userIntern;
        }

        private async Task<List<string>> getListIdUser(List<Forfait_UserIntern> usersForfait)
        {
            var userList = new List<string>();
            foreach (var user in await GetUsersEntity(usersForfait))
            {
                userList.Add(user.user_id);
            }
            return userList;
        }

        private async Task<Result> authDeleteRole(ForfaitClient forfait)
        {
            var usersForfait = await _context.Forfait_Users
                .Where(t => t.IdForfait == forfait.IdForfait)
                .ToListAsync();

            var role=await _context.Roles
                .FindAsync(forfait.RoleId);

            var ids = await getListIdUser(usersForfait);
            var results=new List<Result>();
            foreach(var id in ids)
            {
                results.Add(await _client.DeleteRoleFromUser(id,role.AuthRoleId));
            }
            return authOperationsStatus(results);
        }

        private Result authOperationsStatus(List<Result> results)
        {
            var errors=new List<string>();
            foreach (var result in results)
            {
                if (!result.Succeeded)
                    errors.Add(result.Msg);
                    
            }
            if (errors.Count > 0) return Result.Failure("operations failed", errors);

            return Result.Success("Operation deleting roles susccess");
        }


    }


}
