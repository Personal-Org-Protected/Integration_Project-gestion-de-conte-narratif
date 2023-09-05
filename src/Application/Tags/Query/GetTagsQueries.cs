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

namespace Application.Tags.Query
{

    public record GetTagsQueries() : IRequest<TagVM>;

    public class GetTagsQueriesResponseHandler : IRequestHandler<GetTagsQueries, TagVM>
    {
        private const int _pageSize = 10;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTagsQueriesResponseHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TagVM> Handle(GetTagsQueries request, CancellationToken cancellationToken)
        {

            var tags = await _context.Tag
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.IdTag)
            .ToListAsync();
            return new TagVM()
            {
                Tags = tags,
            };

        }
    }
}
