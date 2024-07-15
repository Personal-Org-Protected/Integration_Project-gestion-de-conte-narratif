using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command.CreateCommand
{

    public record CreateUserCommand(string user_id,string email, string username, string password, string Location, DateTime BirthDate, string phoneNumber, string description) : IRequest<Result>;


    public class CreateUserCommandResponseHandler : IRequestHandler<CreateUserCommand, Result>
    {

        private readonly IApplicationDbContext _context;

        public CreateUserCommandResponseHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User()
            {
                user_id = request.user_id,
                email = request.email,
                username = request.username,
                avatar=""
            };
            _context.Users.Add(entity);
            var result= await _context.SaveChangesAsync(cancellationToken);
//creer library apres la creation de user

            if (result>0) { return Result.Success("User added with success"); }
            return Result.Failure("Action Failed : User could not be added", new List<string>());
        }
    }
}
