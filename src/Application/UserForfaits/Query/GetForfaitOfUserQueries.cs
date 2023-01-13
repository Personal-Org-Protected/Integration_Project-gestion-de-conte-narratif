using Application.Common.Exceptions;
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
    public record GetForfaitOfUserQueries(string user_id) :IRequest<UserForfaitVM>;
    

    public class GetForfaitOfUserQueriesHandler:IRequestHandler<GetForfaitOfUserQueries, UserForfaitVM>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public GetForfaitOfUserQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserForfaitVM> Handle(GetForfaitOfUserQueries request, CancellationToken cancellationToken)
        {

            var user_forfait = await _context.Forfait_Users
               .Where(t => t.user_id == request.user_id)
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
