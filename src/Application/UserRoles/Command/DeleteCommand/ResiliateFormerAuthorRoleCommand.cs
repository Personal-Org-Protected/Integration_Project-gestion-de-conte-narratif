using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRoles.Command.DeleteCommand
{
    public record ResiliateFormerAuthorRoleCommand(string user_id):IRequest<Result>;

    public class ResiliateFormerAuthorRoleCommandHandler : IRequestHandler<ResiliateFormerAuthorRoleCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly int _roleId = 4;

        public ResiliateFormerAuthorRoleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ResiliateFormerAuthorRoleCommand request, CancellationToken cancellationToken)
        {
            var role_user = new Roles_Users()
            {
                idRole = _roleId,
                user_id = request.user_id
            };
            _context.Roles_Users.Add(role_user);
            var result= await _context.SaveChangesAsync(cancellationToken);
            if (result > 0) return Result.Success("role resiliated with success");
            return Result.Failure("role vould not be resiliated",new List<string>());

        }
    }
}
