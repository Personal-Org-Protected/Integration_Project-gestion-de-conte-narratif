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

namespace Application.Users.Command.DeleteCommand
{
 
    public record DeleteUserCommand (string user_id) : IRequest<Result>;

    public class DeleteUserCommandResponseHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IAuth0Client<UserUpdate> _client;
        private readonly IApplicationDbContext _context;

        public DeleteUserCommandResponseHandler(IApplicationDbContext context,IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }
        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _context.beginTransaction();
            var entity = await _context.Users
           .FindAsync(request.user_id) ?? throw new NotFoundException(nameof(Images), request.user_id);


            var result = await DeleteUser(request.user_id, entity, cancellationToken);
            var resultUserRole = await UpdateUserRole(request.user_id,cancellationToken);
            var resultUserForfait=await UpdateUserForfait(request.user_id,cancellationToken);
            if (result.Succeeded && resultUserRole.Succeeded && resultUserForfait.Succeeded) 
            { await _context.commitTransaction(); return Result.Success("User removed with success"); }
            else 
            {
                await _context.rollbackTransaction(); 
                return Result.Failure("Action Failed : User could not be removed", new List<string>()); 
            }
        }

        private async Task<Result> DeleteUser(string user_id,User entity,CancellationToken cancellationToken)
        {
            var authResult = await _client.DeleteUserAsync(user_id);
            _context.Users.Remove(entity);
            _context.userEntities.Remove(await GetUserEntity(user_id));
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask == 2 && authResult.Succeeded) return Result.Success("User deleted");
            return Result.Failure("User could not be deleted",new List<string>() { authResult.Msg});
        }
        private async Task<UserEntity> GetUserEntity(string user_id)
        {
            return await _context.userEntities
                .Where(t=>t.user_id==user_id)
                .SingleOrDefaultAsync()?? throw new NotFoundException();
        }

        private async Task<Result> UpdateUserRole(string user_id ,CancellationToken cancellationToken)
        {
            //var role = await GetRole(idForfait);
            var user_role = await _context.Roles_Users
                .Where(t => t.user_id == user_id)
                .ToListAsync();
            _context.Roles_Users.RemoveRange(user_role);
            var result = await _context.SaveChangesAsync(cancellationToken);
                if (result > 0) return Result.Success("UserRole deleted");
            return Result.Failure("UserRole could not be deleted",new List<string>());
        }
        private async Task<Result> UpdateUserForfait(string user_id, CancellationToken cancellationToken)
        {
            var user=await GetUserEntity(user_id);
            var forfaitUser = await _context.Forfait_Users
                .Where(t => t.IdUser == user.IdUser)
                .ToListAsync();
            _context.Forfait_Users.RemoveRange(forfaitUser);
           var result= await _context.SaveChangesAsync(cancellationToken);
            if (result > 0) return Result.Success("forfait user deleted");
            return Result.Failure("forfait user could not be deleted", new List<string>());

        }
    }
}
