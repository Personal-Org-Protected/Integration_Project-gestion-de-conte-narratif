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
    public record DeleteUserAuthCommand(string user_id):IRequest<Result>;

    public class DeleteUserAuthCommandHandler : IRequestHandler<DeleteUserAuthCommand, Result>
    {
        private readonly IAuth0Client<UserUpdate> _client;
        public DeleteUserAuthCommandHandler(IAuth0Client<UserUpdate> client)
        {
            _client = client;
        }
        public async Task<Result> Handle(DeleteUserAuthCommand request, CancellationToken cancellationToken)
        {
            var authResult = await _client.DeleteUserAsync(request.user_id);
            if (authResult.Succeeded) return Result.Success("User in Auth0 deleted");
            return Result.Failure("User Auth0 could not be deleted", new List<string>() { authResult.Msg });
        }
    }
}
