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
    public record IsUserFormerQueries( ) : IRequest<IsRoleDto>;


    public class IsUserFormerQueriesHandler : IRequestHandler<IsUserFormerQueries, IsRoleDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly int _roleId = 4;

        public IsUserFormerQueriesHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<IsRoleDto> Handle(IsUserFormerQueries request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var isRole = new IsRoleDto()
            {
                id = _roleId
            };
            var userRole = await _context.Roles_Users
                 .Where(t => t.user_id == user_id && t.idRole == _roleId)
                 .SingleOrDefaultAsync();
            if (userRole != null) { isRole.IsRole = true; }
            return isRole;

        }
    }
}
