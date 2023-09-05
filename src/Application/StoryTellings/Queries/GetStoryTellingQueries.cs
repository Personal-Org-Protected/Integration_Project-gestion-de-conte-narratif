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
    public record GetStoryTellingQueries(int pageNumber):IRequest<PaginatedItems<StoryTellingDto>>;

    public class GetStoryTellingQueriesHandler : IRequestHandler<GetStoryTellingQueries, PaginatedItems<StoryTellingDto>>
    {
        private const int _pageSize = 10;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoryTellingQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<StoryTellingDto>> Handle(GetStoryTellingQueries request, CancellationToken cancellationToken)
        {

            return await _context.StoryTellings
            .ProjectTo<StoryTellingDto>(_mapper.ConfigurationProvider)
             .OrderBy(t => t.IdStoryTelling)
              .PaginatedListAsync(request.pageNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no StoryTelling available");
        }
    }
}
