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
 
    public record GetUserQueries(int pgNumber) : IRequest<PaginatedItems<UserDisplay>>;

    public class GetUserQueriesHandler : IRequestHandler<GetUserQueries, PaginatedItems<UserDisplay>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const int _pageSize = 50;

        public GetUserQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<UserDisplay>> Handle(GetUserQueries request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                 .ProjectTo<UserDisplay>(_mapper.ConfigurationProvider)
                 .OrderBy(t => t.username)
                 .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no User available");//de base c'est juste ca a partir de cette ligne

            var ids= new List<int>();
            foreach (var user in users.Items)
            {
                var user_role = await _context.Roles_Users
                    .Where(t => t.user_id == user.user_id).
                    ToListAsync();

                foreach(var role in user_role)
                {
                    ids.Add(role.idRole);
                }

                user.rolesDtos =await _context.Roles
                    .Where(t => ids.Contains(t.IdRole))
                    .ProjectTo<RolesDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

            return users;

        }
    }
}
