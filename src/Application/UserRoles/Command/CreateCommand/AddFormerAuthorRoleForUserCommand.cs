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
    public record AddFormerAuthorRoleForUserCommand() :IRequest<Result>;

    public class AddFormerAuthorRoleForUserCommandHandler : IRequestHandler<AddFormerAuthorRoleForUserCommand, Result>
    {


        private readonly IApplicationDbContext _context;
        private readonly int _roleId=4;
        private readonly IUser _user;
        public AddFormerAuthorRoleForUserCommandHandler( IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Result> Handle(AddFormerAuthorRoleForUserCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var role = new Roles_Users()
            {
                idRole = _roleId,
                user_id = user_id
            };

            _context.Roles_Users.Add(role);
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result > 0) return Result.Success("Role added with success");
            return Result.Failure("Role could not be added", new List<string>());
            
        }
    }
}
