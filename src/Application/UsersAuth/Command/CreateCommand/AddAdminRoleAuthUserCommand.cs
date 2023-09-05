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

namespace Application.UsersAuth.Command.CreateCommand
{

    public record AddAdminRoleAuthUserCommand(string user_id) : IRequest<Result>;



    public class AddAdminRoleAuthUserCommandHandler : IRequestHandler<AddAdminRoleAuthUserCommand, Result>
    {


        private readonly IAuth0Client<UserCreate> _client;
        private readonly IApplicationDbContext _context;
        private readonly int _roleId = 1;
        public AddAdminRoleAuthUserCommandHandler(IAuth0Client<UserCreate> auth0Client, IApplicationDbContext context)
        {
            _context = context;
            _client = auth0Client;
        }

        public async Task<Result> Handle(AddAdminRoleAuthUserCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(_roleId) ?? throw new NotFoundException(); ;
            var result = Result.Success("");// var result = await _client.AddressingRole(request.user_id, role.AuthRoleId);
            return result;
        }
    }
}
