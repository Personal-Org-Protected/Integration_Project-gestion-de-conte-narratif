using Application.Common.Interfaces;
using Application.UserForfaits.Query.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserForfaits.Query
{


    public record GetOwnUserForfaitsQueries() : IRequest<UserForfaitVM>;


    public class GetOwnUserForfaitsQueriesHandler : IRequestHandler<GetOwnUserForfaitsQueries, UserForfaitVM>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        public GetOwnUserForfaitsQueriesHandler(IApplicationDbContext context, IMapper mapper,IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<UserForfaitVM> Handle(GetOwnUserForfaitsQueries request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();

            var user_forfait = await _context.Forfait_Users
               .Where(t => t.user_id == user_id)
               .ToListAsync();

            var ids = new List<int>();
            foreach (var id in user_forfait)
            {
                ids.Add(id.IdForfait);
            }

            return new UserForfaitVM()
            {
                UserForfaits = await _context.Forfaits
                .Where(t => ids.Contains(t.IdForfait))
                .ProjectTo<UserForfaitDto>(_mapper.ConfigurationProvider)
                .ToListAsync()
            };


        }

    }
}
