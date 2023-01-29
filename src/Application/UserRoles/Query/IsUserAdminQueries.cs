using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRoles.Query
{
    public record IsUserAdminQueries(string user_id):IRequest<IsRoleDto>;


    public class IsUserAdminQueriesHandler : IRequestHandler<IsUserAdminQueries, IsRoleDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _roleId = 1;

        public IsUserAdminQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async  Task<IsRoleDto> Handle(IsUserAdminQueries request, CancellationToken cancellationToken)
        {

            var isRole = new IsRoleDto()
            {
                id = _roleId
            };
           var userRole= await _context.Roles_Users
                .Where(t => t.user_id == request.user_id && t.idRole == _roleId)
                .SingleOrDefaultAsync();
            if(userRole != null) { isRole.IsRole = true; }
            return isRole;
        }
    }
}
