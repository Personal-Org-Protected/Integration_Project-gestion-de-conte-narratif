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
    public record ResiliateOwnAuthRoleCommand(int role_id):IRequest<Result>;

    public class ResiliateFormerAuthorAuthRoleCommandHandler : IRequestHandler<ResiliateOwnAuthRoleCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuth0Client<UserUpdate> _client;
        private readonly IUser _user;

        public ResiliateFormerAuthorAuthRoleCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client,IUser user)
        {
            _context = context;
            _client = auth0Client;
            _user = user;
        }

        public async Task<Result> Handle(ResiliateOwnAuthRoleCommand request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var role = await _context.Roles
                .FindAsync(request.role_id) ?? throw new NotFoundException(); ;
            var result = Result.Success("");
            //var result = await _client.DeleteRoleFromUser(user_id, role.AuthRoleId);
            return result;
        }
    }
}
