using Application.Common.Exceptions;
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
    public record GetTagQueries() :IRequest<TagVM>;

    public class GetTagQueriesResponseHandler : IRequestHandler<GetTagQueries, TagVM>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTagQueriesResponseHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TagVM> Handle(GetTagQueries request, CancellationToken cancellationToken)
        {
            return new TagVM
            {
                Tags = await _context.Tag
                .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.IdTag)
                .ToListAsync(cancellationToken) ?? throw new NotFoundException("there is no tag available")
            };
        }
    }
}
