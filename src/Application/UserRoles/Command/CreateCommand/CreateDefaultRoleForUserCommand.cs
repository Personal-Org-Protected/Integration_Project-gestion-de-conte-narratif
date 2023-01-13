using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRoles.Command.CreateCommand
{
    public record CreateDefaultRoleForUserCommand(string user_id):IRequest<Result>;
    public class CreateRoleForUserCommandHandler : IRequestHandler<CreateDefaultRoleForUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly int idRole = 2;

        public CreateRoleForUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateDefaultRoleForUserCommand request, CancellationToken cancellationToken)
        {
            var role_User = new Roles_Users()
            {
                idRole = idRole,
                user_id=request.user_id
            };
            _context.Roles_Users.Add(role_User);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return Result.Success("role added for the user : " + request.user_id);
            return Result.Failure("role could not be added for this user", new List<string>() { "user may already have the forfait" });
        }
    }
}
