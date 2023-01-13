using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.StoryTellings.Queries.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries
{
    public record GetStoryByUserQueries(string user_id,int pgNumber) : IRequest<PaginatedItems<StoryTellingDto>>;

    public class GetStoryByUserQueriesHandler : IRequestHandler<GetStoryByUserQueries, PaginatedItems<StoryTellingDto>>
    {
        private const int _pageSize = 3;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoryByUserQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<StoryTellingDto>> Handle(GetStoryByUserQueries request, CancellationToken cancellationToken)
        {
            return await _context.StoryTellings
                .Where(d => d.user_id == request.user_id)
                .ProjectTo<StoryTellingDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.price)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
        }
    }
}
