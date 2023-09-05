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
    public record ResiliateAuthRoleCommand(string user_id, int roleId) : IRequest<Result>;

    public class ResiliateAuthRoleCommandHandler : IRequestHandler<ResiliateAuthRoleCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuth0Client<UserUpdate> _client;

        public ResiliateAuthRoleCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }

        public async Task<Result> Handle(ResiliateAuthRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(request.roleId) ?? throw new NotFoundException();
            //var result=await _client.DeleteRoleFromUser(request.user_id, role.AuthRoleId);
            var result = Result.Success("");
            return result;
        }
    }

    }
