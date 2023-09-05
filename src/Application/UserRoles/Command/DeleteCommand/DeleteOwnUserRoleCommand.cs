using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRoles.Command.DeleteCommand
{

    public record DeleteOwnUserRoleCommand( int roleId) : IRequest<Result>;

    public class DeleteOwnUserRoleCommandHandler : IRequestHandler<DeleteOwnUserRoleCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        public DeleteOwnUserRoleCommandHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(DeleteOwnUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var role_user = await _context.Roles_Users
                .Where(t => t.user_id == user_id && t.idRole == request.roleId)
                .SingleOrDefaultAsync();
            _context.Roles_Users.Remove(role_user);
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result > 0) return Result.Success("all the user roles have been deleted");

            return Result.Failure("user roles could not be deleted", new List<string>() { "maybe not found" });
        }
    }
}
