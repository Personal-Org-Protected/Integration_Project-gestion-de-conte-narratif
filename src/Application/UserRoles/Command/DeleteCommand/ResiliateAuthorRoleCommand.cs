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

namespace Application.UserRoles.Command.DeleteCommand
{
    public record ResiliateAuthorRoleCommand(string user_id) : IRequest<Result>;



    public class ResiliateAuthorRoleCommandHandler : IRequestHandler<ResiliateAuthorRoleCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly int _roleId = 3;

        public ResiliateAuthorRoleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ResiliateAuthorRoleCommand request, CancellationToken cancellationToken)
        {
          // var forfait=await _context.Forfaits.FindAsync(request.idForfait) ?? throw new NotFoundException();
           var role= await _context.Roles.FindAsync(_roleId) ?? throw new NotFoundException(); ;

            var role_user = await _context.Roles_Users
                .Where(t => t.user_id == request.user_id && t.idRole == role.IdRole)
                .SingleOrDefaultAsync()?? throw new NotFoundException();

            _context.Roles_Users.Remove(role_user);
            var result= await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return Result.Success("Role resiliated with success");
            return Result.Failure("could not resiliate the role", new List<string>());
        }
    }
}
