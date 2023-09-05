using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersAuth.Command.CreateCommand
{

    public record ConfigurationDefaultRole(string user_id) : IRequest<Result>;



    public class ConfigurationDefaultRoleHandler : IRequestHandler<ConfigurationDefaultRole, Result>
    {

        private readonly int idRole = 2;
        private readonly IAuth0Client<UserCreate> _client;
        private readonly IApplicationDbContext _context;
        public ConfigurationDefaultRoleHandler(IAuth0Client<UserCreate> auth0Client, IApplicationDbContext context)
        {
            _context = context;
            _client = auth0Client;
        }



        public async Task<Result> Handle(ConfigurationDefaultRole request, CancellationToken cancellationToken)
        {
            var roleResult = await roleConfigAuth(request.user_id, await getRole(idRole));
            return roleResult;
        }
        private async Task<Result> roleConfigAuth(string user_id, string role_id)
        {
            var result = Result.Success("");//var result = await _client.AddressingRole(user_id, role_id);
            return result;
        }

        private async Task<string> getRole(int idRole)
        {
            var role = await _context.Roles.FindAsync(idRole);
            return role.AuthRoleId;
        }
    }
}
