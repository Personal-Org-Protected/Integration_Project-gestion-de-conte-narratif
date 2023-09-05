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
    public class AdminSendMessageCommand : IRequest<Result>
    {
        public string user_id { get; set; }
        public string title { get; set; }
        public string message { get; set; }
    }

    public class AdminSendMessageCommandHandler : IRequestHandler<AdminSendMessageCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public AdminSendMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(AdminSendMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = new Notification()
            {
                title = request.title,
                message = request.message,
                user_id = request.user_id,
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
