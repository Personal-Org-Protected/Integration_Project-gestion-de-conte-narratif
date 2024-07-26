using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Command.DeleteCommand
{
    public record DeleteNotificationCommand(int id) : IRequest<Result>;

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteNotificationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notifications
            .FindAsync(request.id)??throw new NotFoundException("No Notif found");

            _context.Notifications.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Tag deleted with success");
            return Result.Failure("Action Failed : Tag could not be deleted", new List<string>());
        }
    }
}
