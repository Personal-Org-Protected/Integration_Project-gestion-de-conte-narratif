using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Query
{
    public record GetTagByIdQueries(int id):IRequest<TagDto>;

    public class GetTagByIdQueriesHandler : IRequestHandler<GetTagByIdQueries, TagDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTagByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TagDto> Handle(GetTagByIdQueries request, CancellationToken cancellationToken)
        {
            var Tag = await _context.Tag
                .FindAsync(request.id)?? throw new NotFoundException();

            return _mapper.Map<TagDto>(Tag);
        }
    }
}
