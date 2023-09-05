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
    public record AddFormerAuthorRoleAuthUserCommand():IRequest<Result>;
 
    public class AddFormerAuthorRoleAuthUserCommandHandler : IRequestHandler<AddFormerAuthorRoleAuthUserCommand, Result>
    {


        private readonly IAuth0Client<UserCreate> _client;
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly int _roleId=4;
        public AddFormerAuthorRoleAuthUserCommandHandler(IAuth0Client<UserCreate> auth0Client, IApplicationDbContext context,IUser user)
        {
            _context = context;
            _client = auth0Client;
            _user = user;
        }

        public async Task<Result> Handle(AddFormerAuthorRoleAuthUserCommand request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var role=await _context.Roles.FindAsync(_roleId)?? throw new NotFoundException();
            var result = Result.Success("");// var result=await  _client.AddressingRole(user_id,role.AuthRoleId);
            return result;
        }
    }
}
