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

    public record UpdateUserAuthCommand(string username/*, string Location, string phoneNumber*/) : IRequest<Result>;


    public class UpdateUserAuthCommandHandler : IRequestHandler<UpdateUserAuthCommand, Result>
    {
        private readonly IAuth0Client<UserUpdate> _client;
        private readonly IUser _user;

        public UpdateUserAuthCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client,IUser user)
        {
            _client = auth0Client;
            _user = user;
        }

        public async Task<Result> Handle(UpdateUserAuthCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var userAuth = new UserUpdate()
            {
                username = request.username,
            };

           // return await _client.UpdateUserAsync(userAuth, user_id);
            var result = Result.Success("");
            return result;
        }
    }

}
