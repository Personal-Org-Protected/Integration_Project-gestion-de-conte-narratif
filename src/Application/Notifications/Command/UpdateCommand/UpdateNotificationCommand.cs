using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Command.UpdateCommand
{

    public record UpdateNotificationCommand(int NotifId) : IRequest<Result>;

    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public UpdateNotificationCommandHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var entity = await _context.Notifications.FindAsync(request.NotifId)?? throw new NotFoundException();
            entity.read = true;
            _context.Notifications.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Library added with success");
            }
            return Result.Failure("Action Failed : Library could not be added", new List<string>());
        }
    }
}
