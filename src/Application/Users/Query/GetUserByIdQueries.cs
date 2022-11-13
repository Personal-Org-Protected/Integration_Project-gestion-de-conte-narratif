using Application.Common.Exceptions;
using Application.Common.Interfaces;
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

    public record GetUserByIdQueries(string user_id) : IRequest<UserDisplay>;

    public class GetUserByIdQueriesHandler : IRequestHandler<GetUserByIdQueries, UserDisplay>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDisplay> Handle(GetUserByIdQueries request, CancellationToken cancellationToken)
        {
            var entity= await _context.Users
                .Where(t=>t.user_id==request.user_id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("there is no User available");

            var user = _mapper.Map<UserDisplay>(entity);
            var user_role =  await _context.Roles_Users
                .Where(t => t.user_id == user.user_id)
                .ToListAsync();
            var ids=new List<int>();

            foreach(var id in user_role)
            {
                ids.Add(id.idRole);
            }
            user.rolesDtos=await _context.Roles
                .Where(t=>ids.Contains(t.IdRole))
                .ProjectTo<RolesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return user;


        }
    }
}
