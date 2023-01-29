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


    public record AddAdminRoleForUserCommand(string user_id) : IRequest<Result>;

    public class AddAdminRoleForUserCommandHandler : IRequestHandler<AddAdminRoleForUserCommand, Result>
    {
        private readonly int idRole = 1;
        private readonly IApplicationDbContext _context;

        public AddAdminRoleForUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(AddAdminRoleForUserCommand request, CancellationToken cancellationToken)
        {

            var role_user = new Roles_Users()
            {
                idRole = idRole,
                user_id = request.user_id
            };
            _context.Roles_Users.Add(role_user);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return Result.Success("role added with success");
            return Result.Failure("role could not be added", new List<string>() { "maybe already have" });
        }
    }
}
