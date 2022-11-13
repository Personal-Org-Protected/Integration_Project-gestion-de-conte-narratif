using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forfaits.Queries
{
    public record GetForfaitAuthorQueries(int pgNumber) : IRequest<PaginatedItems<ForfaitDto>>;


    public class GetForfaitAuthorQueriesHandler : IRequestHandler<GetForfaitAuthorQueries, PaginatedItems<ForfaitDto>>
    {

        private const int _pageSize = 5;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetForfaitAuthorQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<ForfaitDto>> Handle(GetForfaitAuthorQueries request, CancellationToken cancellationToken)
        {
            return await _context.Forfaits
           .Where(d => d.IsForAuthor)
           .ProjectTo<ForfaitDto>(_mapper.ConfigurationProvider)
           .OrderBy(t => t.IdForfait)
           .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no Forfait for Author available");
        }
    }
}
