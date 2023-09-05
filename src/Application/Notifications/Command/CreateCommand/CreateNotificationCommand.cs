using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Command.CreateCommand
{
    public class CreateNotificationCommand:IRequest<Result>
    {
        public string title { get; set; }
        public string message { get; set; }
    }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public CreateNotificationCommandHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var entity = new Notification()
            {
                title = request.title,
                message = request.message,
                user_id = user_id,
                created = DateTime.Now,
                read = false
                

            };
            _context.Notifications.Add(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Library added with success");
            }
            return Result.Failure("Action Failed : Library could not be added", new List<string>());
        }
    }
}
