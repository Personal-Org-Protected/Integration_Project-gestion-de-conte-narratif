using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Query.Dto_s;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query
{
    public record GetUserByOwnIdQueries() : IRequest<UserDisplay>;

    public class GetUserByOwnIdQueriesHandler : IRequestHandler<GetUserByOwnIdQueries, UserDisplay>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        public GetUserByOwnIdQueriesHandler(IApplicationDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }
        public async Task<UserDisplay> Handle(GetUserByOwnIdQueries request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var entity = await _context.Users.FindAsync(user_id) ?? throw new NotFoundException("there is no User available");

            return _mapper.Map<UserDisplay>(entity);



        }
    }


}
