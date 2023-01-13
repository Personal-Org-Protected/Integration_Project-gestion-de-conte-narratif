using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
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
    public record GetTagQueries(int pgNumber) :IRequest<PaginatedItems<TagDto>>;

    public class GetTagQueriesResponseHandler : IRequestHandler<GetTagQueries, PaginatedItems<TagDto>>
    {
        private const int _pageSize = 10;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTagQueriesResponseHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<TagDto>> Handle(GetTagQueries request, CancellationToken cancellationToken)
        {

            return await _context.Tag
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.IdTag)
            .PaginatedListAsync(request.pgNumber,_pageSize, cancellationToken) ?? throw new NotFoundException();
           
        }
    }
}
