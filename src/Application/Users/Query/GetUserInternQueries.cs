using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
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
    public record GetUserInternQueries(int pgNumber):IRequest<PaginatedItems<UserInternDto>>;


    public class GetUserInternQueriesHandler : IRequestHandler<GetUserInternQueries, PaginatedItems<UserInternDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const int _pageSize = 50;

        public GetUserInternQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<UserInternDto>> Handle(GetUserInternQueries request, CancellationToken cancellationToken)
        {
            var users= await _context.userEntities
                .ProjectTo<UserInternDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.IdUser)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no User Intern available");

            var ids = new List<int>();
            foreach (var user in users.Items)
            {
                var user_role = await _context.Forfait_Users
                    .Where(t => t.IdUser == user.IdUser)
                    .ToListAsync();

                foreach (var role in user_role)
                {
                    ids.Add(role.IdForfait);
                }

                user.forfaitDtos = await _context.Forfaits
                    .Where(t => ids.Contains(t.IdForfait))
                    .ProjectTo<ForfaitDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

            return users;
        }
    }

}
