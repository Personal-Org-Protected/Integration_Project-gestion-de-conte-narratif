using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersAuth.Command.CreateCommand.CreateUserAuthCommand
{
    public record CreateUserAuthCommand(string user_id,string email, string username, string password, string Location, DateTime BirthDate, string phoneNumber,string description) : IRequest<Result>;



    public class CreateUserAuthCommandHandler : IRequestHandler<CreateUserAuthCommand, Result>
    {
        private const string connection = "Username-Password-Authentication";
        private readonly IAuth0Client<UserCreate> _client;
        private readonly IApplicationDbContext _context;
        public CreateUserAuthCommandHandler( IAuth0Client<UserCreate> auth0Client, IApplicationDbContext context)
        {
            _context = context;
            _client = auth0Client;
        }
        public async Task<Result> Handle(CreateUserAuthCommand request, CancellationToken cancellationToken)
        {
            var entityAuth = new UserCreate()
            {
                user_id = request.user_id,
                username = request.username,
                email = request.email,
                password = request.password,
                connection = connection
            };
            var result=  await _client.CreateUserAsync(entityAuth);
            if (result.Succeeded)return Result.Success("Creation  user auth avec succes");

            return Result.Failure("Impossible de creer le user sur auth",new List<string>() { result.Msg});
        }

    }
}
