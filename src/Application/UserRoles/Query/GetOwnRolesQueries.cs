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
    public record GetOwnRolesQueries() : IRequest<UserRolesVM>;

    public class GetOwnRolesQueriesHandler : IRequestHandler<GetOwnRolesQueries, UserRolesVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public GetOwnRolesQueriesHandler(IApplicationDbContext context, IMapper mapper,IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }
        public async Task<UserRolesVM> Handle(GetOwnRolesQueries request, CancellationToken cancellationToken)
        {
            var  user_id=_user.getUserId();

            var user_roles = await _context.Roles_Users
               .Where(t => t.user_id ==user_id)
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
