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
    public record AddAuthorRoleForUserCommand():IRequest<Result>;

    public class AddAuthorRoleForUserHandler : IRequestHandler<AddAuthorRoleForUserCommand, Result>
    {
        private readonly int idRole = 3;
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        public AddAuthorRoleForUserHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(AddAuthorRoleForUserCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var role_user = new Roles_Users()
            {
                idRole = idRole,
                user_id = user_id
            };
            _context.Roles_Users.Add(role_user);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return Result.Success("role added with success");
            return Result.Failure("role could not be added", new List<string>() { "maybe already have" });
        }
    }


}
