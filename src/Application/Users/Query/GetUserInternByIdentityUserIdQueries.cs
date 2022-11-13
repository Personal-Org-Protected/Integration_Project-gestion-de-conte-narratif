using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Query.Dto_s;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query
{
    public record GetUserInternByIdentityUserIdQueries(string user_id):IRequest<UserInternDto>;


    public class GetUserInternByIdentityUserIdQueriesHandler : IRequestHandler<GetUserInternByIdentityUserIdQueries, UserInternDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUserInternByIdentityUserIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserInternDto> Handle(GetUserInternByIdentityUserIdQueries request, CancellationToken cancellationToken)
        {
            var entity = await _context.userEntities
               .Where(t => t.user_id == request.user_id)
               .FirstOrDefaultAsync() ?? throw new NotFoundException("there is no User available");

            var user = _mapper.Map<UserInternDto>(entity);

            var user_forfait = await _context.Forfait_Users
               .Where(t => t.IdUser == user.IdUser)
               .ToListAsync();

            var ids = new List<int>();
            foreach (var id in user_forfait)
            {
                ids.Add(id.IdForfait);
            }

            user.forfaitDtos = await _context.Forfaits
                .Where(t => ids.Contains(t.IdForfait))
                .ProjectTo<ForfaitDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return user;
        }
    }
}
