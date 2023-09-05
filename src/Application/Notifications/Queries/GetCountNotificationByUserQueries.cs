using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Queries
{
    public record GetCountNotificationByUserQueries() : IRequest<int>;

    public class GetCountNotificationByUserQueriesHandler : IRequestHandler<GetCountNotificationByUserQueries,int>
    {
        private const int _pageSize = 4;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        public GetCountNotificationByUserQueriesHandler(IApplicationDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }
        public async Task<int> Handle(GetCountNotificationByUserQueries request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var notifications = await _context.Notifications
                .Where(d => d.user_id == user_id && !d.read)
                .ToListAsync();

            return notifications.Count;
        }
    }
}
