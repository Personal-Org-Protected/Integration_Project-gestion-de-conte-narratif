using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRoles.Query
{
    public record GetRolesOfUserQueries(string user_id) :IRequest<UserRolesVM>;

    public class GetRolesOfUserQueriesHandler : IRequestHandler<GetRolesOfUserQueries, UserRolesVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRolesOfUserQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserRolesVM> Handle(GetRolesOfUserQueries request, CancellationToken cancellationToken)
        {


            var user_roles = await _context.Roles_Users
               .Where(t => t.user_id == request.user_id)
               .ToListAsync();

            var ids = new List<int>();


                foreach (var role in user_roles)
                {
                    ids.Add(role.idRole);
                }


            return new UserRolesVM()
            {
                UserRoles = await _context.Roles
                    .Where(t => ids.Contains(t.IdRole))
                    .ProjectTo<UserRolesDto>(_mapper.ConfigurationProvider)
                    .ToListAsync()
            };
        }
    }

}
