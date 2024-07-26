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
    public record GetUserSimpleInfoQueries(string user_id) : IRequest<UserSimpleInfoDto>;

    public class GetUserSimpleInfoQueriesHandler : IRequestHandler<GetUserSimpleInfoQueries, UserSimpleInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserSimpleInfoQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserSimpleInfoDto> Handle(GetUserSimpleInfoQueries request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FindAsync(request.user_id) ?? throw new NotFoundException("there is no User available");

            return _mapper.Map<UserSimpleInfoDto>(entity);



        }
    }
}
