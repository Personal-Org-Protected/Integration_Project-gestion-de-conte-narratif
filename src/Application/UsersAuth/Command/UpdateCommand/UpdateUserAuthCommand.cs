using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersAuth.Command.UpdateCommand
{

    public record UpdateUserAuthCommand(string user_id, string username/*, string Location, string phoneNumber*/) : IRequest<Result>;


    public class UpdateUserAuthCommandHandler : IRequestHandler<UpdateUserAuthCommand, Result>
    {
        private readonly IAuth0Client<UserUpdate> _client;

        public UpdateUserAuthCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client)
        {
            _client = auth0Client;
        }

        public async Task<Result> Handle(UpdateUserAuthCommand request, CancellationToken cancellationToken)
        {
            var userAuth = new UserUpdate()
            {
                username = request.username,
            };

            return await _client.UpdateUserAsync(userAuth, request.user_id);
       
        }
    }

}
