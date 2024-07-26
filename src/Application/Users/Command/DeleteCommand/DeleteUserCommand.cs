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

namespace Application.Users.Command.DeleteCommand
{
 
    public record DeleteUserCommand (string user_id) : IRequest<Result>;

    public class DeleteUserCommandResponseHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUserCommandResponseHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.Users
           .FindAsync(request.user_id) ?? throw new NotFoundException(nameof(User), request.user_id);

            _context.Users.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);
            if (resultTask > 0) return Result.Success("User deleted");
            return Result.Failure("User could not be deleted", new List<string>() { });

        }

 
    }
}
