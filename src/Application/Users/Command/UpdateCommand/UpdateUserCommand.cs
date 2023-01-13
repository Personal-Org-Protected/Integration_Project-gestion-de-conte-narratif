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

namespace Application.Users.Command.UpdateCommand
{
    public record UpdateUserCommand(string user_id, string username, string Location, string phoneNumber, string description) : IRequest<Result>;


    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var Entity = await _context.Users
                .FindAsync(request.user_id) ?? throw new NotFoundException();

            Entity.Location = request.Location;
            Entity.username = request.username;
            Entity.phoneNumber = request.phoneNumber;
            Entity.description = request.description;
            _context.Users.Update(Entity);
            var result= await _context.SaveChangesAsync(cancellationToken);


            if (result>0) { return Result.Success("User modified with success"); }
            return Result.Failure("Action Failed : User could not be Modified", new List<string>());
        }

    }
}
