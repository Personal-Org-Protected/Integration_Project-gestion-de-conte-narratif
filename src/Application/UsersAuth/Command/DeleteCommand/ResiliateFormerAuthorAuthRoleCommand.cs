using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersAuth.Command.DeleteCommand
{
    public record ResiliateFormerAuthorAuthRoleCommand(string user_id):IRequest<Result>;

    public class ResiliateFormerAuthorAuthRoleCommandHandler : IRequestHandler<ResiliateFormerAuthorAuthRoleCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuth0Client<UserUpdate> _client;
        private readonly int _roleId = 5;

        public ResiliateFormerAuthorAuthRoleCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }

        public async Task<Result> Handle(ResiliateFormerAuthorAuthRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles
                .FindAsync(_roleId) ?? throw new NotFoundException(); ;
            var result = await _client.DeleteRoleFromUser(request.user_id, role.AuthRoleId);
            return result;
        }
    }
}
