using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Queries
{
    public record GetNotificationsByUserQueries(int pgNumber) : IRequest<PaginatedItems<NotificationDto>>;

    public class GetNotificationsByUserQueriesHandler : IRequestHandler<GetNotificationsByUserQueries, PaginatedItems<NotificationDto>>
    {
        private const int _pageSize = 10;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        public GetNotificationsByUserQueriesHandler(IApplicationDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }
        public async Task<PaginatedItems<NotificationDto>> Handle(GetNotificationsByUserQueries request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var notifications = await _context.Notifications
                .Where(d => d.user_id == user_id && !d.read)
                .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.created)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no Notification available");

            return notifications;
        }
    }
}
