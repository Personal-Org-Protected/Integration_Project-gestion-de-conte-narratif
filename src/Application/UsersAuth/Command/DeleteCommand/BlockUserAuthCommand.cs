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


    public record BlockUserAuthCommand(string user_id, string username) : IRequest<Result>;

    public class BlockUserAuthCommandHandler : IRequestHandler<BlockUserAuthCommand, Result>
    {
        private readonly IAuth0Client<UserUpdate> _client;
        public BlockUserAuthCommandHandler(IAuth0Client<UserUpdate> client)
        {
            _client = client;
        }
        public async Task<Result> Handle(BlockUserAuthCommand request, CancellationToken cancellationToken)
        {

            var userAuth = new UserUpdate()
            {
                username = request.username,
                blocked = true
            };

           //await _client.UpdateUserAsync(userAuth, request.user_id);
            var result =Result.Success("");
            return result;
        }
    }
}
